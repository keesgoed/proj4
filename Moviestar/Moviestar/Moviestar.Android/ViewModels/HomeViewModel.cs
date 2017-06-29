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
    class HomeViewModel
    {
        public HomeViewModel()
        {

        }

        public List<Models.Movie> LoadAllItemFromMySQL()
        {
            List<Models.Movie> movielist = new List<Models.Movie>();
            try
            {
                string connsqlstring = "server = sql11.freemysqlhosting.net; port = 3306; database = sql11182336; uid = sql11182336; pwd = qhJhLGfTnt; charset = utf8;";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet movies = new DataSet();
                string queryString = "select * FROM Movie WHERE imdb_score > 7.5 ORDER BY RAND() LIMIT 10";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(movies, "Item");
                foreach (DataRow row in movies.Tables["Item"].Rows)
                {
                    Models.Movie generated_movie = new Models.Movie()
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