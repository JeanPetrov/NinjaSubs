namespace NinjaSubs.Data.Models
{
    public class GenreSubtitles
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int SubtitlesId { get; set; }
        public Subtitles Subtitles { get; set; }
    }
}
