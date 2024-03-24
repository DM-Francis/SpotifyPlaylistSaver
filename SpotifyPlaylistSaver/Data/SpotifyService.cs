using SpotifyAPI.Web;
using SpotifyPlaylistSaver.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyPlaylistSaver.Data
{
    public class SpotifyService
    {
        private readonly SpotifyTokenProvider _tokenProvider;
        private SpotifyClient _spotifyClient;
        private string _userId;

        public SpotifyService(SpotifyTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task InitializeAsync()
        {
            string access_token = _tokenProvider.AccessToken;
            _spotifyClient = new SpotifyClient(access_token);

            var profile = await _spotifyClient.UserProfile.Current();
            _userId = profile.Id;
        }

        public async Task<List<string>> GetPlaylistsAsync()
        {
            var pagedPlaylists = await _spotifyClient.Playlists.CurrentUsers();

            return pagedPlaylists.Items.Select(p => p.Name).ToList();
        }

        public async Task<List<string>> GetDiscoverWeeklyTracks()
        {
            string discoverWeeklyId = await GetDiscoverWeeklyPlaylistId();

            var pagedTracks = await _spotifyClient.Playlists.GetItems(discoverWeeklyId);
            return pagedTracks.Items
                .ToFullTracks()
                .Select(x => x.Name)
                .ToList();
        }

        private async Task<string> GetDiscoverWeeklyPlaylistId()
        {
            var pagedPlaylists = await _spotifyClient.Playlists.CurrentUsers();
            var discoverWeeklyPl = pagedPlaylists.Items.FirstOrDefault(p => p.Name == "Discover Weekly");

            return discoverWeeklyPl?.Id;
        }

        public async Task SaveDiscoverWeeklyPlaylist()
        {
            string discoverWeeklyId = await GetDiscoverWeeklyPlaylistId();
            var pagedTracks = await _spotifyClient.Playlists.GetItems(discoverWeeklyId);

            var discoverCreatedate = pagedTracks.Items?.Min(x => x.AddedAt);
            List<string> trackUris = pagedTracks.Items.ToFullTracks().Select(x => x.Uri).ToList();

            string newPlaylistName = $"Saved Weekly {discoverCreatedate:dd/MM/yy}";

            var playlistCreateRequest = new PlaylistCreateRequest(newPlaylistName) { Public = false };
            var newPlaylist = await _spotifyClient.Playlists.Create(_userId, playlistCreateRequest);
            await _spotifyClient.Playlists.AddItems(newPlaylist.Id, new PlaylistAddItemsRequest(trackUris));
        }

    }
}
