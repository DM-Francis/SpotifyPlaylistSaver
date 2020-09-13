using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using SpotifyPlaylistSaver.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyPlaylistSaver.Data
{
    public class SpotifyService
    {
        private readonly SpotifyTokenProvider _tokenProvider;
        private SpotifyWebAPI _spotifyWebAPI;
        private string _userId;

        public SpotifyService(SpotifyTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task InitializeAsync()
        {
            string access_token = _tokenProvider.AccessToken;

            _spotifyWebAPI = new SpotifyWebAPI
            {
                AccessToken = access_token,
                TokenType = "Bearer"
            };

            var profile = await _spotifyWebAPI.GetPrivateProfileAsync();
            _userId = profile.Id;
        }

        public async Task<List<string>> GetPlaylistsAsync()
        {
            Paging<SimplePlaylist> pagedPlaylists = await _spotifyWebAPI.GetUserPlaylistsAsync(_userId);

            return pagedPlaylists.Items.Select(p => p.Name).ToList();
        }

        public async Task<List<string>> GetDiscoverWeeklyTracks()
        {
            string discoverWeeklyId = await GetDiscoverWeeklyPlaylistId();

            var pagedTracks = await _spotifyWebAPI.GetPlaylistTracksAsync(discoverWeeklyId);
            return pagedTracks.Items.Select(t => t.Track.Name).ToList();
        }

        private async Task<string> GetDiscoverWeeklyPlaylistId()
        {
            Paging<SimplePlaylist> pagedPlaylists = await _spotifyWebAPI.GetUserPlaylistsAsync(_userId);
            var discoverWeeklyPl = pagedPlaylists.Items.Where(p => p.Name == "Discover Weekly").FirstOrDefault();

            return discoverWeeklyPl?.Id;
        }

        public async Task SaveDiscoverWeeklyPlaylist()
        {
            string discoverWeeklyId = await GetDiscoverWeeklyPlaylistId();
            var pagedTracks = await _spotifyWebAPI.GetPlaylistTracksAsync(discoverWeeklyId);

            DateTime discoverCreatedate = pagedTracks.Items.Min(t => t.AddedAt);
            List<string> trackUris = pagedTracks.Items.Select(t => t.Track.Uri).ToList();

            string newPlaylistName = $"Saved Weekly {discoverCreatedate:dd/MM/yy}";
            var newPlaylist = await _spotifyWebAPI.CreatePlaylistAsync(_userId, newPlaylistName, isPublic: false);
            await _spotifyWebAPI.AddPlaylistTracksAsync(newPlaylist.Id, trackUris);
        }
    }
}
