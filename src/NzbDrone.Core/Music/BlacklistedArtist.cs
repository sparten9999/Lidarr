using System.Collections.Generic;

namespace NzbDrone.Core.Music
{
    public static class BlacklistedArtist
    {
        private readonly static Dictionary<string, string> BlacklistedArtists = new Dictionary<string, string>
        {
            { "89ad4ac3-39f7-470e-963a-56509c546377", "Various Artist" }
        };

        public static bool CheckBlacklisted(string mbid)
        {
            return BlacklistedArtists.ContainsKey(mbid);
        }
    }
}
