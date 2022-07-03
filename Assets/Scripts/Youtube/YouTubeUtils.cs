using System;
using System.Web;

/// <summary>
/// Utility functions for interacting with YouTube.
/// </summary>
public static class YouTubeUtils
{
    /// <summary>
    /// Pulls the video ID from a YouTube URL.
    /// </summary>
    /// <param name="youTubeUrl">YouTube URL to extract ID from.</param>
    /// <returns>Canonical YouTube video ID (e.g. VZBYoN-iHkE).</returns>
    public static string ExtractVideoId(string youTubeUrl)
    {
        if (string.IsNullOrEmpty(youTubeUrl))
        {
            return null;
        }
        
        // YouTube URLs come in a few different formats.
        // Normalize the URL such that the video ID appears in the query string.
        youTubeUrl = youTubeUrl
            .Trim()
            .Replace("youtu.be/", "youtube.com/watch?v=")
            .Replace("youtube.com/embed/", "youtube.com/watch?v=")
            .Replace("/watch#", "/watch?");

        if (youTubeUrl.Contains("/v/"))
        {
            var absolutePath = new Uri(youTubeUrl).AbsolutePath;
            absolutePath = absolutePath.Replace("/v/", "/watch?v=");
            youTubeUrl = $"https://youtube.com{absolutePath}";
        }

        // The URL should now contain a query string of the format v={video-id}.
        var queryString = new Uri(youTubeUrl).Query;
        var query = HttpUtility.ParseQueryString(queryString);
        return query.Get("v");
    }
    
    /// <summary>
    /// Normalizes a YouTube URL to the format https://youtube.com/watch?v={video-id}.
    /// </summary>
    /// <param name="youTubeUrl">YouTube URL to normalize.</param>
    /// <param name="normalizedYouTubeUrl">Normalized YouTube URL.</param>
    /// <returns>Whether normalization was successful and the URL is valid.</returns>
    public static bool TryNormalizeYouTubeUrl(string youTubeUrl, out string normalizedYouTubeUrl)
    {
        var videoId = ExtractVideoId(youTubeUrl);

        if (string.IsNullOrEmpty(videoId))
        {
            normalizedYouTubeUrl = null;
            return false;
        }

        normalizedYouTubeUrl = $"https://www.youtube.com/watch?v={videoId}&gl=US&hl=en&has_verified=1&bpctr=9999999999";
        return true;
    }
}