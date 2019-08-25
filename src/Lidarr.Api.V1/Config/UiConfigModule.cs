using Nancy.Configuration;
﻿using System.Linq;
using System.Reflection;
using NzbDrone.Core.Configuration;
using Lidarr.Http;

namespace Lidarr.Api.V1.Config
{
    public class UiConfigModule : LidarrConfigModule<UiConfigResource>
    {
        public UiConfigModule(INancyEnvironment environment, IConfigService configService)
            : base(environment,  configService)
        {

        }

        protected override UiConfigResource ToResource(IConfigService model)
        {
            return UiConfigResourceMapper.ToResource(model);
        }
    }
}