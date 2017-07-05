using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moviestar.ViewModels;
using Moviestar.Interfaces;

namespace Moviestar.UWP
{
    class MovieList
    {
        public MovieList()
        {
            addData();
        }
        public string Test { set; get; } = "test waarde";


        public void addData()
        {
            HomeViewModel movies = new HomeViewModel();
            foreach (var movie in movies.GetMovies())
            {
                Test = movie.actor_1_name;
            }
        }
    }
}
