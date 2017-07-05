using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moviestar.ViewModels;
using Moviestar.Interfaces;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Moviestar.UWP
{
    class RatedMovies
    {
        public List<Models.Movie> movie { get; set; }

        public RatedMovies()
        {
            movie = new List<Models.Movie>();

            RatedMovieViewModel movies = new RatedMovieViewModel();
            foreach (var moviedata in movies.GetMovies("1"))
            {
                movie.Add(new Models.Movie(moviedata.id, moviedata.actor_1_name, moviedata.actor_2_name, moviedata.actor_3_name, moviedata.genres, moviedata.title, moviedata.imdb_score, moviedata.rating));
            }

        }

    }
}

