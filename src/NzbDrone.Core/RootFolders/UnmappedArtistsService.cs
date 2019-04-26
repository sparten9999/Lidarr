using System.Collections.Generic;
using System.Linq;
using NLog;
using NzbDrone.Core.MediaFiles;
using NzbDrone.Core.MediaFiles.TrackImport;
using NzbDrone.Core.MediaFiles.TrackImport.Identification;

namespace NzbDrone.Core.RootFolders
{
    public interface IUnmappedArtistsService
    {
        List<UnmappedArtist> Get(string path);
    }

    public class UnmappedArtistsService :
        IUnmappedArtistsService
    {
        private readonly IDiskScanService _diskScanService;
        private readonly IMakeImportDecision _importDecisionMaker;
        private readonly IIdentificationService _identificationService;
        private readonly Logger _logger;

        public UnmappedArtistsService(IDiskScanService diskScanService,
                                      IMakeImportDecision importDecisionMaker,
                                      IIdentificationService identificationService,
                                      Logger logger)
        {
            _diskScanService = diskScanService;
            _importDecisionMaker = importDecisionMaker;
            _identificationService = identificationService;
            _logger = logger;
        }

        public List<UnmappedArtist> Get(string path)
        {
            var files = _diskScanService.FilterFiles(path, _diskScanService.GetAudioFiles(path));
            var localTracks = _importDecisionMaker.GetLocalTracks(files, null, null, FilterFilesType.Matched).Item1;
            var tracksWithoutArtists = _identificationService.TracksWithoutArtists(localTracks);

            return tracksWithoutArtists.Where(x => x.FileTrackInfo != null)
                .GroupBy(x => x.FileTrackInfo.ArtistTitle)
                .Select(x => new UnmappedArtist {
                        Name = x.First().FileTrackInfo.ArtistTitle,
                        Tracks = x.Select(y => y.FileTrackInfo).ToList()
                    })
                .ToList();
        }
    }
}
