using System.Collections.Generic;
using System.Linq;
using SpotifyAPI.Web;

namespace SpotifyPlaylistSaver.Data;

public static class SpotifyExtensions
{
    public static IEnumerable<FullTrack> ToFullTracks(
        this IEnumerable<PlaylistTrack<IPlayableItem>> playlistTracks)
    {
        return playlistTracks
            .Where(x => x.Track.Type == ItemType.Track)
            .Select(x => (FullTrack)x.Track);
    }
}