namespace MovieAggregator.TMDb.ApiInteraction.Helpers
{
    public static class TMDbImageHelper
    {
        const string ImageSizeParam = "w500";

        public static string GetImageUrl(string posterPath)
        {
            try
            {
                var uri = TMDbClientHelper.GetConfifuredClient().GetImageUrl(ImageSizeParam, posterPath);
                if (uri != null)
                {
                    return uri.AbsoluteUri;
                }
            }
            catch
            {
                //log exception
            }

            return null;
        }
    }
}