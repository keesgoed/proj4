using System;

using Moviestar.Models;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using System.Data;
using Android.Graphics;
using Android.Util;
using System.Collections.Generic;
using Moviestar.Droid.ViewModels;
using Moviestar.ViewModels;
using Moviestar.Interfaces;


namespace Moviestar.Droid
{
    [Activity(Label = "Moviestar", MainLauncher = true, Icon = "@drawable/star")]
    public class MovieList : Activity
    {

        int rating;
        private LinearLayout scrollBlock;
        String selectedItem;
        String movie_ID = "1";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MovieList);

            //Create navigation
            createSpinner();

            // linking variables to axml
            scrollBlock = FindViewById<LinearLayout>(Resource.Id.ScrollLayout);

            // get Data from DB and make dynamic movieblocks
            MakeBlocks();

            //get movie ID
            movie_ID = Intent.GetStringExtra("MovieID") ?? "Data not available";
          
            //Search functionality
            CreateSearchBar();

            //Create android controls instance
            androidControls android_controls = new androidControls();

            // Set android Controls to work with the global adapter
            IControls AndroidAdapter = new androidControlsAdapter(android_controls);
        }

        //create spinner
        public void createSpinner()
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.menu_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        public void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            if (Convert.ToString(spinner.GetItemAtPosition(e.Position)) != this.selectedItem)
            {
                selectedItem = Convert.ToString(spinner.GetItemAtPosition(e.Position));

                // If statement to correctly navigate
                if (selectedItem == "Search")
                {
                    StartActivity(typeof(Search));
                }
                if (selectedItem == "Recommended movies")
                {
                    StartActivity(typeof(MovieList));
                }
                if (selectedItem == "Login")
                {
                    StartActivity(typeof(Login));
                }
                if (selectedItem == "Rated Movies")
                {
                    StartActivity(typeof(RatedMovies));
                }
            }
        }

        public void MakeBlocks()
        {
            HomeViewModel movies = new HomeViewModel();
            foreach (var movie in movies.GetMovies())
            {
                // Set the Linearlayout for movie blocks
                LinearLayout movieBlock = new LinearLayout(this);
                movieBlock.Orientation = Orientation.Vertical;
                movieBlock.SetBackgroundColor(new Color(60, 60, 60));
                LinearLayout.LayoutParams linearLayoutParamsBlock = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                linearLayoutParamsBlock.SetMargins(0, 40, 0, 0);
                movieBlock.LayoutParameters = linearLayoutParamsBlock;
                movieBlock.SetPadding(0, 5, 0, 0);


                // New LinearLayout to hold the image and description
                LinearLayout movieBlockCover = new LinearLayout(this);
                movieBlockCover.Orientation = Orientation.Horizontal;
                LinearLayout.LayoutParams linearLayoutParamsCover = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                linearLayoutParamsCover.SetMargins(15, 15, 0, 0);
                movieBlockCover.LayoutParameters = linearLayoutParamsCover;

                // Set the Titles and the axml
                TextView title = new TextView(this);
                title.SetTextSize(ComplexUnitType.Px, 45);
                title.SetTextColor(new Color(255, 255, 255));
                title.SetPadding(10, 10, 0, 0);
                title.Text = movie.title.ToString();

                // Set Info Button
                Button infobtn = new Button(this);
                infobtn.Id = Int32.Parse(movie.id);
                infobtn.Text = "See Movie Info";
                int btnId = infobtn.Id;

                // Set Description and the axml
                TextView desc = new TextView(this);
                desc.SetPadding(100, 0, 0, 0);
                desc.SetTextColor(new Color(255, 255, 255));
                desc.SetTextSize(ComplexUnitType.Px, 24);
                desc.Text = movie.actor_1_name.ToString() + "\n" + movie.actor_2_name.ToString() + "\n" + movie.actor_3_name.ToString() + " \n\ngenres: " + movie.genres.ToString() + "\n \nIMDB score: " + movie.imdb_score.ToString();


                // Set Image view
                ImageView movieCover = new ImageView(this);
                movieCover.SetImageResource(Resource.Drawable.cover);
                LinearLayout.LayoutParams linearLayoutParamsImage = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);
                linearLayoutParamsImage.Width = 250;
                linearLayoutParamsImage.Height = 350;
                movieCover.LayoutParameters = linearLayoutParamsImage;
                movieCover.SetPadding(15, 15, 0, 60);

                // add Elements to the main scrollview
                movieBlock.AddView(title);
                movieBlockCover.AddView(movieCover);
                movieBlockCover.AddView(desc);
                movieBlock.AddView(movieBlockCover);
                movieBlock.AddView(infobtn);

                //add go to movie page button
                infobtn.Click += delegate
                {
                    var MoviePage = new Intent(this, typeof(MoviePage));
                    String Movie_ID = movie.id;
                    MoviePage.PutExtra("MovieID", Movie_ID);

                    StartActivity(MoviePage);                  
                };

                scrollBlock.AddView(movieBlock);
            }
        }
        //create search
        public void CreateSearchBar()
        {
            SearchView searchBar = FindViewById<SearchView>(Resource.Id.searchBar);
            searchBar.QueryTextSubmit += (s, e) =>
            {
                //TODO: Do something fancy when search button on keyboard is pressed
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;

                var UserInput = new Intent(this, typeof(Search));
                string search = e.Query;
                UserInput.PutExtra("UserInput", search);

                StartActivity(UserInput);
            };

        }

    }
}



