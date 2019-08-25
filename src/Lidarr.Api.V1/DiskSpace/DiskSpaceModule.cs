using Nancy.Configuration;
﻿using System.Collections.Generic;
using NzbDrone.Core.DiskSpace;
using Lidarr.Http;

namespace Lidarr.Api.V1.DiskSpace
{
    public class DiskSpaceModule :LidarrRestModule<DiskSpaceResource>
    {
        private readonly IDiskSpaceService _diskSpaceService;

        public DiskSpaceModule(INancyEnvironment environment, IDiskSpaceService diskSpaceService)
            :base(environment,  "diskspace")
        {
            _diskSpaceService = diskSpaceService;
            GetResourceAll = GetFreeSpace;
        }

        public List<DiskSpaceResource> GetFreeSpace()
        {
            return _diskSpaceService.GetFreeSpace().ConvertAll(DiskSpaceResourceMapper.MapToResource);
        }
    }
}
