using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moviestar.ViewModels;
using Moviestar.Interfaces;
using System.Diagnostics;

namespace Moviestar.UWP
{
    class MovieList
    {

        public string Test { set; get; } = "test waarde";
        private List<Models.Movie> unfilteredMovie = new List<Models.Movie>();

        public List<Models.Movie> movie { get; set; }


        public MovieList()
        {
            movie = new List<Models.Movie>();

            HomeViewModel movies = new HomeViewModel();
            foreach (var moviedata in movies.GetMovies())
            {
                Debug.WriteLine("Dit is een test printline " + moviedata.id);
                movie.Add(new Models.Movie(moviedata.id, moviedata.actor_1_name, moviedata.actor_2_name, moviedata.actor_3_name, moviedata.genres, moviedata.title, moviedata.imdb_score));
                Debug.WriteLine(movie[0].title);
            }
  
        }



    }
}
