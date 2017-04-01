using Google.Apis.YouTube.v3.Data;
using System;

namespace VideoProvider.Youtube.ApiInteraction
{
    public static class SearchResultExtensions
    {
        public static bool TitleContains(this SearchResult result, string matchString)
        {
            if (matchString == null)
                throw new ArgumentException($"{nameof(matchString)} parameter cannot be null"); 

            if (result == null || result.Snippet == null || result.Snippet.Title == null)
                return false;

            return result.Snippet.Title.IndexOf(matchString, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}