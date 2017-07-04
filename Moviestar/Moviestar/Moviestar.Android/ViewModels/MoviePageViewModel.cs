﻿using System;
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
    class MoviePageViewModel
    {
        string movieId;
        public MoviePageViewModel(string m)
        {
            movieId = m;
        }

        public List<Models.Movie> LoadAllItemFromMySQL()
        {
            List<Models.Movie> movielist = new List<Models.Movie>();
            try
            {
                string connsqlstring = "server = sql11.freemysqlhosting.net; port = 3306; database = sql11183344; uid = sql11183344; pwd = zHZXlUfr4L; charset = utf8;";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet moviePage = new DataSet();
                string queryString = "select * FROM movies WHERE movie_id = '" + movieId + "'";
                Console.WriteLine(queryString);

                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(moviePage, "Item");
                foreach (DataRow row in moviePage.Tables["Item"].Rows)
                {
                    Models.Movie generated_moviePage = new Models.Movie()
                    {
                        id = (row[0].ToString()),
                        director_name = (row[1].ToString()),
                        duration = (row[2].ToString()),
                        actor_1_name = (row[3].ToString()),
                        actor_2_name = (row[4].ToString()),
                        actor_3_name = (row[5].ToString()),
                        genres = (row[6].ToString()),
                        title = (row[7].ToString()),
                        language = (row[8].ToString()),
                        title_year = (row[9].ToString()),
                        imdb_score = (row[10].ToString()),
                    };
                    movielist.Add(generated_moviePage);

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