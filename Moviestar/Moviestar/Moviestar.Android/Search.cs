﻿using System;

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
using Moviestar.ViewModels;
using Moviestar.Interfaces;
using Moviestar.IteratorPattern;

namespace Moviestar.Droid
{
    [Activity(Label = "Search results")]
    public class Search : Activity
    {
        private LinearLayout scrollBlock;
        String selectedItem;
        String userInput;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Search);

            //create spinner
            createSpinner();

            //Get Search Input
            userInput = Intent.GetStringExtra("UserInput");

            // linking variables to axml
            scrollBlock = FindViewById<LinearLayout>(Resource.Id.ScrollLayout2);

            //Make dynamic movieblocks
            createResultBox();

            //Search functionality
            CreateSearchBar();

            //Create android controls instance
            androidControls android_controls = new androidControls();

            // Set android Controls to work with the global adapter
            IControls AndroidAdapter = new androidControlsAdapter(android_controls);
        }

        public void createResultBox()
        {
            // use Iterator to iterate through list of movie objects
            SearchViewModel movielist = new SearchViewModel();
            Iterator<Models.Movie> movies = new IterableList<Models.Movie>(movielist.GetMovies(userInput));
            Models.Movie movie = movies.First();
            while (movie != null)
            {
                // Set the Linearlayout for movie blocks
                LinearLayout movieBlock = new LinearLayout(this);
                movieBlock.Orientation = Orientation.Vertical;
                movieBlock.SetBackgroundColor(new Color(60, 60, 60));
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
                title.SetTextColor(new Color(255, 255, 255));
                title.SetPadding(5, 0, 0, 0);
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
                linearLayoutParamsImage.Width = 200;
                linearLayoutParamsImage.Height = 300;
                movieCover.LayoutParameters = linearLayoutParamsImage;
                movieCover.SetPadding(15, 0, 0, 60);

                // add Elements to the main scrollview
                movieBlock.AddView(title);
                movieBlockCover.AddView(movieCover);
                movieBlockCover.AddView(desc);
                movieBlock.AddView(movieBlockCover);
                movieBlock.AddView(infobtn);

                String Movie_ID = movie.id;
                //add go to movie page button
                infobtn.Click += delegate
                {
                    var MoviePage = new Intent(this, typeof(MoviePage));
                    
                    MoviePage.PutExtra("MovieID", Movie_ID);

                    StartActivity(MoviePage);
                };

                scrollBlock.AddView(movieBlock);

                // go to the next Movie Object
                movie = movies.Next();
            }
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