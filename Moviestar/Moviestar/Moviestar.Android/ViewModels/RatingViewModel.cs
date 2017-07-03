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
    class RatingViewModel
    {
        string user_id;
        string movie_id;
        int rating;
    
        public RatingViewModel(string user_id, string movie_id, int rating)
        {
            this.user_id = user_id;
            this.movie_id = movie_id;
            this.rating = rating;

        }

        public List<Models.Rating> LoadAllItemFromMySQL()
        {
            List<Models.Rating> movielist = new List<Models.Rating>();
            try
            {
                string connsqlstring = "server = sql11.freemysqlhosting.net; port = 3306; database = sql11182336; uid = sql11182336; pwd = qhJhLGfTnt; charset = utf8;";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                // Insert info in database
                DataSet rating = new DataSet();
                string queryString = "INSERT INTO rating(movie_id, user_id, rating) VALUES (" + movie_id + ", " + user_id + ", " + rating + ")";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(rating, "Item");

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