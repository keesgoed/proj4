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
using Moviestar.Droid.ViewModels;

namespace Moviestar.Droid
{
    [Activity(Label = "Moviestar", MainLauncher = true, Icon = "@drawable/star")]
    public class Search : Activity
    {
        int rating;
        private LinearLayout scrollBlock;
        String selectedItem;
        String movie_ID = "1";
        String userInput;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.Search);

            //create spinner
            createSpinner();

            // linking variables to axml
            scrollBlock = FindViewById<LinearLayout>(Resource.Id.ScrollLayout);

            // get Data from DB and make dynamic movieblocks
            //MakeBlocks();

            //Get Search Input
            userInput = Intent.GetStringExtra("UserInput");

            Console.WriteLine("GJHGJHGHJGHGHGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG" + userInput);
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
            Console.WriteLine("spinner_ItemSelected() " + this.selectedItem);
            Spinner spinner = (Spinner)sender;

            if (Convert.ToString(spinner.GetItemAtPosition(e.Position)) != this.selectedItem)
            {
                selectedItem = Convert.ToString(spinner.GetItemAtPosition(e.Position));

                Console.WriteLine("Changeview() " + this.selectedItem);
                // If statement to correctly navigate
                if (selectedItem == "Search")
                {
                    Console.WriteLine("in de search if statement " + this.selectedItem);
                    StartActivity(typeof(Search));
                    createSpinner();
                }
                if (selectedItem == "Recommended movies")
                {
                    Console.WriteLine("in de recommended movies if statement " + this.selectedItem);
                    StartActivity(typeof(MovieList));
                    createSpinner();
                }
                if (selectedItem == "Login")
                {
                    Console.WriteLine("in de login if statement " + this.selectedItem);
                    StartActivity(typeof(Login));
                    createSpinner();

                    Console.WriteLine("Writeline in the if statement " + this.selectedItem);
                }
                Console.WriteLine(selectedItem);

            }
        }

        public void MakeBlocks()
        {
            SearchViewModel movies = new SearchViewModel(userInput);
            foreach (var movie in movies.LoadAllItemFromMySQL())
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
                title.Text = movie.title.ToString();

                // Set Info Button
                Button infobtn = new Button(this);
                infobtn.Id = Int32.Parse(movie.id);
                infobtn.Text = "See Movie Info";
                int btnId = infobtn.Id;

                // Set Description and the axml
                TextView desc = new TextView(this);
                desc.SetPadding(100, 0, 0, 0);
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

                //add go to movie page button
                infobtn.Click += delegate
                {
                    var MoviePage = new Intent(this, typeof(MoviePage));
                    String Movie_ID = movie.id;
                    MoviePage.PutExtra("MovieID", Movie_ID);


                    // Change view + create dropdown spinner
                    //SetContentView(Resource.Layout.MoviePage);
                    StartActivity(MoviePage);
                    createSpinner();


                };

                scrollBlock.AddView(movieBlock);
            }
        }

    }
}