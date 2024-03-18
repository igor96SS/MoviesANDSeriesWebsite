namespace Movies_SeriesWebsite.Models
{
    public class SeriesModel
    {
        public int serieID { get; set; }
        public string serieName { get; set; }
        public int? startYear { get; set; }
        public int? endYear { get; set; }
        public bool ownSeason { get; set; }
        public int? lastSeason { get; set; }
        public int? lastEpisode { get; set; }
    }
}
