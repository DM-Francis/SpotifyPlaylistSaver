using Microsoft.AspNetCore.Authentication.OAuth;

namespace SpotifyPlaylistSaver.Authentication
{
    public class SpotifyOAuthOptions : OAuthOptions
    {
        public SpotifyOAuthOptions()
        {
            AuthorizationEndpoint = SpotifyAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = SpotifyAuthenticationDefaults.TokenEndpoint;
        }
    }
}
