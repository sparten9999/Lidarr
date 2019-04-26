using System.Collections.Generic;
using FluentValidation;
using NzbDrone.Core.RootFolders;
using NzbDrone.Core.Validation.Paths;
using NzbDrone.SignalR;
using Lidarr.Http;

namespace Lidarr.Api.V1.RootFolders
{
    public class RootFolderModule : LidarrRestModuleWithSignalR<RootFolderResource, RootFolder>
    {
        private readonly IRootFolderService _rootFolderService;
        private readonly IUnmappedArtistsService _unmappedArtistsService;

        public RootFolderModule(IRootFolderService rootFolderService,
                                IUnmappedArtistsService unmappedArtistsService,
                                IBroadcastSignalRMessage signalRBroadcaster,
                                RootFolderValidator rootFolderValidator,
                                PathExistsValidator pathExistsValidator,
                                MappedNetworkDriveValidator mappedNetworkDriveValidator,
                                StartupFolderValidator startupFolderValidator,
                                SystemFolderValidator systemFolderValidator,
                                FolderWritableValidator folderWritableValidator
        )
            : base(signalRBroadcaster)
        {
            _rootFolderService = rootFolderService;
            _unmappedArtistsService = unmappedArtistsService;

            GetResourceAll = GetRootFolders;
            GetResourceById = GetRootFolder;
            CreateResource = CreateRootFolder;
            DeleteResource = DeleteFolder;

            SharedValidator.RuleFor(c => c.Path)
                           .Cascade(CascadeMode.StopOnFirstFailure)
                           .IsValidPath()
                           .SetValidator(rootFolderValidator)
                           .SetValidator(mappedNetworkDriveValidator)
                           .SetValidator(startupFolderValidator)
                           .SetValidator(pathExistsValidator)
                           .SetValidator(systemFolderValidator)
                           .SetValidator(folderWritableValidator);
        }

        private RootFolderResource GetRootFolder(int id)
        {
            return _rootFolderService.Get(id).ToResource();
        }

        private int CreateRootFolder(RootFolderResource rootFolderResource)
        {
            var model = rootFolderResource.ToModel();

            return _rootFolderService.Add(model).Id;
        }

        private List<RootFolderResource> GetRootFolders()
        {
            var folders = _rootFolderService.AllWithSpaceStats().ToResource();
            folders.ForEach(x => x.UnmappedArtists = _unmappedArtistsService.Get(x.Path));
            return folders;
        }

        private void DeleteFolder(int id)
        {
            _rootFolderService.Remove(id);
        }
    }
}
