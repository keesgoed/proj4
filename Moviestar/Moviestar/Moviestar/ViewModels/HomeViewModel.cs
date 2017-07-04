using Moviestar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {

        }

        public List<Movie> GetMovies()
        {
            List<Movie> movielist = new List<Movie>();
            var httpconnect = new Connection();
            string jsonresponse = httpconnect.GetAPI();
            movielist = JsonConvert.DeserializeObject<List<Movie>>(jsonresponse);
            return movielist;
        }
    }
}
