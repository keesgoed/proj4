using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moviestar.Models;
using Newtonsoft.Json;

namespace Moviestar.ViewModels
{
    public class MoviePageViewModel
    {
        public MoviePageViewModel()
        {

        }
        public List<Movie> GetMovies(string id)
        {
            List<Movie> movielist = new List<Movie>();
            var httpconnect = new Connection();
            string jsonresponse = httpconnect.GetAPI("select_moviepage/" + id);
            movielist = JsonConvert.DeserializeObject<List<Movie>>(jsonresponse);
            return movielist;
        }

        public void RateMovie(string user_id, string movie_id, int rating)
        {
            var httpconnect = new Connection();
            httpconnect.GetAPI("ratemovie/" + user_id + "/" + movie_id + "/" + rating.ToString() );
        }
    }
}
