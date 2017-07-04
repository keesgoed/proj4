using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using System.Data;

namespace Moviestar.Droid.ViewModels
{
    class RatedMovieViewModel
    {
        public RatedMovieViewModel()
        {

        }

        public List<Models.Movie> LoadAllItemFromMySQL()
        {
            List<Models.Movie> movielist = new List<Models.Movie>();
            try
            {
                string connsqlstring = "server = sql11.freemysqlhosting.net; port = 3306; database = sql11183344; uid = sql11183344; pwd = zHZXlUfr4L; charset = utf8;";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet movies = new DataSet();
                string queryString = "SELECT movies.movie_title,movies.imdb_score, movies.genres, ratings.rating FROM movies, ratings WHERE movies.movie_id = ratings.movie_id AND ratings.user_id = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(movies, "Item");
                foreach (DataRow row in movies.Tables["Item"].Rows)
                {
                    Models.Movie generated_movie = new Models.Movie()
                    {
                        title = (row[0].ToString()),
                        imdb_score = (row[1].ToString()),
                        genres = (row[2].ToString()),
                        rating = (row[3].ToString())
                        
                    };

                    movielist.Add(generated_movie);

                }

                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return movielist;

        }
    }
}