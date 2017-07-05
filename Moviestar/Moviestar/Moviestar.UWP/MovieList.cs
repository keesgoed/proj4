﻿using System;
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
    class MovieList
    {

        public List<Models.Movie> movie { get; set; }

        public MovieList()
        {
            movie = new List<Models.Movie>();

            HomeViewModel movies = new HomeViewModel();
            foreach (var moviedata in movies.GetMovies())
            {
                movie.Add(new Models.Movie(moviedata.id, moviedata.actor_1_name, moviedata.actor_2_name, moviedata.actor_3_name, moviedata.genres, moviedata.title, moviedata.imdb_score, moviedata.rating));
            }
  
        }
        // Selected movie object _selectedMovie
        private Models.Movie _selectedMovie;

        public Models.Movie selectedMovie
        {
            get { return _selectedMovie; }

            set {
                    _selectedMovie = value;
                Debug.WriteLine(_selectedMovie.title);

            }
        }
    }
}
