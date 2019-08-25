using System;
using System.Runtime.Loader;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NzbDrone.Common.EnvironmentInfo;
using NzbDrone.Common.Messaging;
using TinyIoC;
using System.IO;

namespace NzbDrone.Common.Composition
{
    public abstract class ContainerBuilderBase
    {
        private readonly List<Type> _loadedTypes;

        protected IContainer Container { get; }

        protected ContainerBuilderBase(IStartupContext args, List<string> assemblies)
        {
            _loadedTypes = new List<Type>();

            assemblies.Add(OsInfo.IsWindows ? "Lidarr.Windows" : "Lidarr.Mono");
            assemblies.Add("Lidarr.Common");
           assemblies.Add("Mono.Posix");

            var path = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var assembly in assemblies)
            {
//                var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
//                Console.WriteLine(path);
                _loadedTypes.AddRange(AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(path, $"{assembly}.dll")).GetTypes());
//                Assembly.Load(assembly);
            }

            Container = new Container(new TinyIoCContainer(), _loadedTypes);
            AutoRegisterInterfaces();
            Container.Register(args);
       }

        private void AutoRegisterInterfaces()
        {
            var loadedInterfaces = _loadedTypes.Where(t => t.IsInterface).ToList();
            var implementedInterfaces = _loadedTypes.SelectMany(t => t.GetInterfaces());

            var contracts = loadedInterfaces.Union(implementedInterfaces).Where(c => !c.IsGenericTypeDefinition && !string.IsNullOrWhiteSpace(c.FullName))
                .Where(c => !c.FullName.StartsWith("System"))
                .Except(new List<Type> { typeof(IMessage), typeof(IEvent), typeof(IContainer) }).Distinct().OrderBy(c => c.FullName);

            foreach (var contract in contracts)
            {
                AutoRegisterImplementations(contract);
            }
        }

        protected void AutoRegisterImplementations<TContract>()
        {
            AutoRegisterImplementations(typeof(TContract));
        }

        private void AutoRegisterImplementations(Type contractType)
        {
            var implementations = Container.GetImplementations(contractType).Where(c => !c.IsGenericTypeDefinition).ToList();

            if (implementations.Count == 0)
            {
                return;
            }
            if (implementations.Count == 1)
            {
                var impl = implementations.Single();
                Container.RegisterSingleton(contractType, impl);
            }
            else
            {
                Container.RegisterAllAsSingleton(contractType, implementations);
            }
        }
    }
}
