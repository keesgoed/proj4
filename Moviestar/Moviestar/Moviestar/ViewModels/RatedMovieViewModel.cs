using Moviestar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.ViewModels
{
    public class RatedMovieViewModel
    {
        public RatedMovieViewModel()
        {

        }

        public List<Movie> GetMovies(string user_id)
        {
            List<Movie> movielist = new List<Movie>();
            var httpconnect = new Connection();
            string jsonresponse = httpconnect.GetAPI("select_ratedmovies/" + user_id);
            movielist = JsonConvert.DeserializeObject<List<Movie>>(jsonresponse);
            return movielist;
        }
    }
}
