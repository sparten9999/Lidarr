using Nancy.Configuration;
using FluentValidation;
using NzbDrone.Core.Configuration;
using Lidarr.Http.Validation;

namespace Lidarr.Api.V1.Config
{
    public class IndexerConfigModule : LidarrConfigModule<IndexerConfigResource>
    {

        public IndexerConfigModule(INancyEnvironment environment, IConfigService configService)
            : base(environment,  configService)
        {
            SharedValidator.RuleFor(c => c.MinimumAge)
                           .GreaterThanOrEqualTo(0);

            SharedValidator.RuleFor(c => c.MaximumSize)
                           .GreaterThanOrEqualTo(0);

            SharedValidator.RuleFor(c => c.Retention)
                           .GreaterThanOrEqualTo(0);

            SharedValidator.RuleFor(c => c.RssSyncInterval)
                           .IsValidRssSyncInterval();
        }

        protected override IndexerConfigResource ToResource(IConfigService model)
        {
            return IndexerConfigResourceMapper.ToResource(model);
        }
    }
}
