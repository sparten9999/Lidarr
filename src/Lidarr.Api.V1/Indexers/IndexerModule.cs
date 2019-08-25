using Nancy.Configuration;
﻿using NzbDrone.Core.Indexers;

namespace Lidarr.Api.V1.Indexers
{
    public class IndexerModule : ProviderModuleBase<IndexerResource, IIndexer, IndexerDefinition>
    {
        public static readonly IndexerResourceMapper ResourceMapper = new IndexerResourceMapper();

        public IndexerModule(INancyEnvironment environment, IndexerFactory indexerFactory)
            : base(environment,  indexerFactory, "indexer", ResourceMapper)
        {
        }

        protected override void Validate(IndexerDefinition definition, bool includeWarnings)
        {
            if (!definition.Enable) return;
            base.Validate(definition, includeWarnings);
        }
    }
}