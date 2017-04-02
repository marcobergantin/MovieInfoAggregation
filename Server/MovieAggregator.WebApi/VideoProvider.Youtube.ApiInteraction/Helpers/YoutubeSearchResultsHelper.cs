using Google.Apis.YouTube.v3.Data;
using System.Collections.Generic;
using System.Linq;
using VideoProvider.Youtube.ApiInteraction.Extensions;

namespace VideoProvider.Youtube.ApiInteraction.Helpers
{
    public static class YoutubeSearchResultsHelper
    {
        public static SearchResult FilterSearchResults(string movieTitle, string yearString, IList<SearchResult> results)
        {
            var subset = results.Where(r => r.TitleContains(movieTitle));
            if (subset == null || subset.Count() == 0)
            {
                subset = results;
            }

            if (string.IsNullOrWhiteSpace(yearString) == false)
            {
                var yearInTitleSubset = subset.Where(r => r.Snippet.Title.Contains(yearString));
                if (yearInTitleSubset != null && yearInTitleSubset.Count() > 0)
                {
                    subset = yearInTitleSubset;
                }

                var yearInDescriptionTooSubset = subset.Where(r => r.Snippet.Description.Contains(yearString));
                if (yearInDescriptionTooSubset != null && yearInDescriptionTooSubset.Count() > 0)
                {
                    subset = yearInDescriptionTooSubset;
                }
            }

            return subset.FirstOrDefault();
        }
    }
}