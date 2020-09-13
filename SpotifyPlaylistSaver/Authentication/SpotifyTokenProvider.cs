using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyPlaylistSaver.Authentication
{
    public class SpotifyTokenProvider
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
