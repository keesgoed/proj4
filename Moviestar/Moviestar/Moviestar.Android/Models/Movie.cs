using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.Models
{
    public class Movie
    {
        public int id { get; set; }
        public string director_name { get; set; }
        public int duration { get; set; }
        public string genres { get; set; }
        public string title { get; set; }
        public string language { get; set; }
        public int title_year { get; set; }
        public double imdb_score { get; set; }
        public string box_cover { get; set; }

    }
}