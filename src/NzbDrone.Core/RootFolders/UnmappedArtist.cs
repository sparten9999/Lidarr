using System.Collections.Generic;
using NzbDrone.Core.Parser.Model;

namespace NzbDrone.Core.RootFolders
{
    public class UnmappedArtist
    {
        public string Name { get; set; }
        public List<ParsedTrackInfo> Tracks { get; set; }
    }
}
