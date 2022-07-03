using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public enum YouTubeRequestResult
{
    InProgress, 
    Error, 
    Success,
}

/// <summary>
/// Provides a method to find URLs to a YouTube video's raw video files.
/// </summary>
public class YouTubeRequest
{
    public string Error { get; private set; }
    public YouTubeRequestResult Result { get; private set; }

    public YouTubePlayerResponse.StreamingData.Format BestQualityFormat => Formats
        .OrderByDescending(format => format.bitrate)
        //.First();
		.Last();//Tomo la mas baja calidad
    
    public List<YouTubePlayerResponse.StreamingData.Format> Formats => _playerResponse.streamingData.formats
        .Where(format => format.IsCompatible)
        .ToList();

    private YouTubePlayerResponse _playerResponse;
    private readonly string _youTubeUrl;

    /// <summary>
    /// Creates a request to find a YouTube video files.
    /// </summary>
    /// <param name="youTubeUrl">YouTube video URL.</param>
    public YouTubeRequest(string youTubeUrl)
    {
        if (!YouTubeUtils.TryNormalizeYouTubeUrl(youTubeUrl, out _youTubeUrl))
        {
            throw new ArgumentException("Invalid YouTube URL.", nameof(youTubeUrl));
        }
    }

    /// <summary>
    /// Downloads a list of video files for a YouTube video.
    /// </summary>
    public IEnumerator SendRequest()
    {
        Result = YouTubeRequestResult.InProgress;
        Debug.Log($"Fetching YouTube page source from {_youTubeUrl}");

        // Fetch the page source. That is, get the HTML that the browser downloads when you visit a YouTube page.
        string pageSource;
        
        using (var request = UnityWebRequest.Get(_youTubeUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                ReportError($"Error fetching YouTube page: {request.error}");
                yield break;
            }

            pageSource = request.downloadHandler.text;
        }

        // Extract video details from the page HTML.
        var playerResponse = YouTubePlayerResponse.FromPageSource(pageSource);

        if (!playerResponse.HasValue)
        {
            ReportError("Unable to parse video details from YouTube page.");
            yield break;
        }

        _playerResponse = playerResponse.Value;
        Debug.Log($"Downloaded details for YouTube video: \"{playerResponse.Value.videoDetails.title}\"");

        if (!playerResponse.Value.IsPlayable)
        {
            ReportError($"YouTube video unplayable: {playerResponse.Value.playabilityStatus.reason}");
            yield break;
        }

        Result = YouTubeRequestResult.Success;
    }
    
    private void ReportError(string error)
    {
        Error = error;
        Result = YouTubeRequestResult.Error;
    }
}