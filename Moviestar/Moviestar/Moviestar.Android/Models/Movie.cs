using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.Models
{
    public class Movie
    {
        public string id { get; set; }
        public string director_name { get; set; }
        public string duration { get; set; }
        public string actor_1_name { get; set; }
        public string actor_2_name { get; set; }
        public string actor_3_name { get; set; }
        public string genres { get; set; }
        public string title { get; set; }
        public string language { get; set; }
        public string title_year { get; set; }
        public string imdb_score { get; set; }

    }
}