using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifyPlaylistSaver.Authentication;
using System.Threading.Tasks;

namespace SpotifyPlaylistSaver.Pages
{
    public class HostModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string SpotifyAccessToken { get; set; }

        public HostModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGet()
        {
            SpotifyAccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(SpotifyAuthenticationDefaults.AuthenticationScheme, "access_token");
        }
    }
}
