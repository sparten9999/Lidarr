using Nancy.Configuration;
using NzbDrone.Core.Configuration;

namespace Lidarr.Api.V1.Config
{
    public class DownloadClientConfigModule : LidarrConfigModule<DownloadClientConfigResource>
    {
        public DownloadClientConfigModule(INancyEnvironment environment, IConfigService configService)
            : base(environment,  configService)
        {
        }

        protected override DownloadClientConfigResource ToResource(IConfigService model)
        {
            return DownloadClientConfigResourceMapper.ToResource(model);
        }
    }
}
