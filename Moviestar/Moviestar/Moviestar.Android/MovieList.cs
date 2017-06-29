using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using Android.Graphics;
using Android.Util;

namespace Moviestar.Droid
{
    [Activity(Label = "Moviestar.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private LinearLayout scrollBlock;
        String selectedItem = "Recommended movies";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.MovieList);

            //add go to movie page button
            Button MoviePageButton = FindViewById<Button>(Resource.Id.moviePageButton);
            MoviePageButton.Click += delegate {
                StartActivity(typeof(MoviePage));
            };

            //create spinner
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.menu_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            // linking variables to axml
            scrollBlock = FindViewById<LinearLayout>(Resource.Id.ScrollLayout);
            // get Data from DB and make dynamic movieblocks
            LoadAllItemFromMySQL();
        }

		
        public void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Console.WriteLine(this.selectedItem);
            if (Convert.ToString(spinner.GetItemAtPosition(e.Position)) != this.selectedItem)
            {
                changeView();
                Console.WriteLine("Writeline in the if statement " + this.selectedItem);
                this.selectedItem = Convert.ToString(spinner.GetItemAtPosition(e.Position));
            }
            Console.WriteLine(this.selectedItem);
        }

        private void changeView()
        {

            if (this.selectedItem == "Search")
            {
                // SetContentView(Resource.Layout.Search);
            }
            if (this.selectedItem == "Recommended movies")
            {
                SetContentView(Resource.Layout.MovieList);
            }
            if (this.selectedItem == "Login")
            {
                //  SetContentView(Resource.Layout.Login);
            }
        }

        public void LoadAllItemFromMySQL()
        {
            try
            {
                string connsqlstring = "server = sql11.freemysqlhosting.net; port = 3306; database = sql11182336; uid = sql11182336; pwd = qhJhLGfTnt; charset = utf8;";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet movies = new DataSet();
                string queryString = "select movie_title, genres FROM Movie ORDER BY imdb_score LIMIT 10";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(movies, "Item");
                foreach (DataRow row in movies.Tables["Item"].Rows)
                {
                    // Set the Linearlayout for movie blocks
                    LinearLayout movieBlock = new LinearLayout(this);
                    movieBlock.Orientation = Orientation.Vertical;
                    movieBlock.SetBackgroundColor(new Color(52, 152, 219));
                    LinearLayout.LayoutParams linearLayoutParamsBlock = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                    linearLayoutParamsBlock.SetMargins(0, 25, 0, 0);
                    movieBlock.LayoutParameters = linearLayoutParamsBlock;
                    movieBlock.SetPadding(0, 5, 0, 0);


                    // New LinearLayout to hold the image and description
                    LinearLayout movieBlockCover = new LinearLayout(this);
                    movieBlockCover.Orientation = Orientation.Horizontal;
                    LinearLayout.LayoutParams linearLayoutParamsCover = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                    linearLayoutParamsCover.SetMargins(0, 15, 0, 0);
                    movieBlockCover.LayoutParameters = linearLayoutParamsCover;

                    // Set the Titles and the axml
                    TextView title = new TextView(this);
                    title.SetTextSize(ComplexUnitType.Px, 30);
                    title.SetPadding(5, 0, 0, 0);
                    title.Text = row[0].ToString();

                    // Set star rating button
                    ImageView ratingbtn = new ImageView(this);
                    ratingbtn.SetImageResource(Resource.Drawable.star);
                    LinearLayout.LayoutParams linearLayoutParamsRating = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                    linearLayoutParamsRating.Width = 40;
                    linearLayoutParamsRating.Height = 40;
                    ratingbtn.LayoutParameters = linearLayoutParamsRating;
                    ratingbtn.SetPadding(15, 15, 0, 0);

                    // Set Description and the axml
                    TextView desc = new TextView(this);
                    desc.SetPadding(100, 0, 0, 0);
                    desc.SetTextSize(ComplexUnitType.Px, 24);
                    desc.Text = row[1].ToString();

                    // Set Image view
                    ImageView movieCover = new ImageView(this);
                    movieCover.SetImageResource(Resource.Drawable.cover);
                    LinearLayout.LayoutParams linearLayoutParamsImage = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                    linearLayoutParamsImage.Width = 150;
                    linearLayoutParamsImage.Height = 250;
                    movieCover.LayoutParameters = linearLayoutParamsImage;
                    movieCover.SetPadding(15, 0, 0, 60);

                    // add Elements to the main scrollview
                    movieBlock.AddView(title);
                    movieBlock.AddView(ratingbtn);
                    movieBlockCover.AddView(movieCover);
                    movieBlockCover.AddView(desc);
                    movieBlock.AddView(movieBlockCover);
                    scrollBlock.AddView(movieBlock);

                }

                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }


    }
}



