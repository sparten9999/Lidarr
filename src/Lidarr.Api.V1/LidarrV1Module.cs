using Nancy;
using Nancy.Configuration;

namespace Lidarr.Api.V1
{
    public abstract class LidarrV1Module : NancyModule
    {
        protected readonly INancyEnvironment _environment;

        protected LidarrV1Module(INancyEnvironment environment,
                                 string resource)
            : base("/api/v1/" + resource.Trim('/'))
        {
            _environment = environment;
        }
    }
}
