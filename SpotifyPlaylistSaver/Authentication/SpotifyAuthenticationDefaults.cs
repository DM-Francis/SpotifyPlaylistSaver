namespace SpotifyPlaylistSaver.Authentication
{
    public static class SpotifyAuthenticationDefaults
    {
        public static string AuthenticationScheme => "Spotify";
        public static string AuthorizationEndpoint => "https://accounts.spotify.com/authorize";
        public static string TokenEndpoint => "https://accounts.spotify.com/api/token";

    }
}
