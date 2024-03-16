using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_SeriesWebsite.Models
{
    public class MoviesModel
    {
        public int id { get; set; }
        public string movieName { get; set; }
        public int? year { get; set; }
        public bool own { get; set; }
    }
}
