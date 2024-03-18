namespace Movies_SeriesWebsite.Models
{
    public class SeriesModel
    {
        public int SerieID { get; set; }
        public string SerieName { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? OwnSeason { get; set; }
        public int? LastSeason { get; set; }
        public int? LastEpisode { get; set; }
    }
}
