using System;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Holds the result of parsing the ytInitialPlayerResponse JSON from a YouTube page.
/// </summary>
/// <remarks>
/// This is an incomplete list of fields in ytInitialPlayerResponse.
/// The full object contains many more, but we only care about a few.
/// </remarks>
[Serializable]
public struct YouTubePlayerResponse
{
    [Serializable]
    public struct PlayabilityStatus
    {
        public string reason;
        public string status;
    }
    
    [Serializable]
    public struct StreamingData
    {
        [Serializable]
        public struct Format
        {
            public int bitrate;
            public string fps;
            public string mimeType;
            public string quality;
            public string url;

            /// <summary>
            /// Whether this format is compatible with Unity's VideoPlayer.
            /// </summary>
            // TODO: this is probably needlessly restrictive
            public bool IsCompatible => mimeType.Contains("video/mp4");
        }

        public Format[] formats;
    }
    
    [Serializable]
    public struct VideoDetails
    {
        public string shortDescription;
        public string title;
    }
    
    public PlayabilityStatus playabilityStatus;
    public StreamingData streamingData;
    public VideoDetails videoDetails;

    // Example of unplayable video: https://www.youtube.com/watch?v=qm5q1o7ofnc
    public bool IsPlayable => playabilityStatus.status != "ERROR";

    public static YouTubePlayerResponse FromJson(string json)
    {
        return JsonUtility.FromJson<YouTubePlayerResponse>(json);
    }

    public static YouTubePlayerResponse? FromPageSource(string pageSource)
    {
        // Extract the JSON from the JavaScript in the HTML.
        var regex = new Regex(@"ytInitialPlayerResponse\s*=\s*(\{.+?\})\s*;", RegexOptions.Multiline);
        var match = regex.Match(pageSource);

        if (!match.Success)
        {
            return null;
        }
        
        var json = match.Result("$1");
        return FromJson(json);
    }
}