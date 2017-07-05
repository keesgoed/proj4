using Moviestar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {

        }
        public List<Movie> GetMovies(string input)
        {
            List<Movie> movielist = new List<Movie>();
            var httpconnect = new Connection();
            string jsonresponse = httpconnect.GetAPI("select_searchresults/" + input);
            movielist = JsonConvert.DeserializeObject<List<Movie>>(jsonresponse);
            return movielist;
        }
    }
}
