using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyPlaylistSaver.Authentication
{
    public static class SpotifyAuthenticationDefaults
    {
        public static string AuthenticationScheme => "Spotify";
        public static string AuthorizationEndpoint => "https://accounts.spotify.com/authorize";
        public static string TokenEndpoint => "https://accounts.spotify.com/api/token";

    }
}
