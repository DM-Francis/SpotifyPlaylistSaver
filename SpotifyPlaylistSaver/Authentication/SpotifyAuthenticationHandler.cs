using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SpotifyPlaylistSaver.Authentication
{
    public class SpotifyAuthenticationHandler : OAuthHandler<SpotifyOAuthOptions>
    {
        public SpotifyAuthenticationHandler(IOptionsMonitor<SpotifyOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }
    }
}
