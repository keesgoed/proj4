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
        public string rating { get; set; }


        public Movie(string id, string actor_1_name, string actor_2_name, string actor_3_name, string genres, string title, string imdb_score)
        {
            this.id = id;
            this.actor_1_name = actor_1_name;
            this.actor_2_name = actor_2_name;
            this.actor_3_name = actor_3_name;
            this.genres = genres;
            this.title = title;
            this.imdb_score = imdb_score;
        }
    }
}