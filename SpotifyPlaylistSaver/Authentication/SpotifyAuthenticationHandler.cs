using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace SpotifyPlaylistSaver.Authentication
{
    public class SpotifyAuthenticationHandler : OAuthHandler<SpotifyOAuthOptions>
    {
        public SpotifyAuthenticationHandler(IOptionsMonitor<SpotifyOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }
    }
}
