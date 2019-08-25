using Nancy.Configuration;
﻿using System.Collections.Generic;
using System.IO;
using NzbDrone.Common.Disk;
using NzbDrone.Common.EnvironmentInfo;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Configuration;

namespace Lidarr.Api.V1.Logs
{
    public class LogFileModule : LogFileModuleBase
    {
        private readonly IAppFolderInfo _appFolderInfo;
        private readonly IDiskProvider _diskProvider;

        public LogFileModule(INancyEnvironment environment, IAppFolderInfo appFolderInfo,
                             IDiskProvider diskProvider,
                             IConfigFileProvider configFileProvider)
            : base(environment,  diskProvider, configFileProvider, "")
        {
            _appFolderInfo = appFolderInfo;
            _diskProvider = diskProvider;
        }

        protected override IEnumerable<string> GetLogFiles()
        {
            return _diskProvider.GetFiles(_appFolderInfo.GetLogFolder(), SearchOption.TopDirectoryOnly);
        }

        protected override string GetLogFilePath(string filename)
        {
            return Path.Combine(_appFolderInfo.GetLogFolder(), filename);
        }

        protected override string DownloadUrlRoot
        {
            get
            {
                return "logfile";
            }
        }

    }
}