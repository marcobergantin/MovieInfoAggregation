namespace MovieAggregator.Client.DTOs
{
    public class MovieTrailerDTO
    {
        public string VideoTitle { get; set; }
        public string VideoURL { get; set; }

        public string GetEmbedUrl()
        {
            return "https://www.youtube.com/embed/" + 
                VideoURL.Substring(VideoURL.LastIndexOf('?') + 3);
        }
    }
}
