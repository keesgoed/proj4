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

namespace Moviestar.Droid
{
    [Activity(Label = "Moviestar", MainLauncher = true, Icon = "@drawable/star")]
    public class MainActivity : Activity
    {

        int rating;

        private LinearLayout scrollBlock;
        String selectedItem = "Recommended movies";
        String movie_ID = "1";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.MovieList);

            //create spinner
            createSpinner();

            // linking variables to axml
            scrollBlock = FindViewById<LinearLayout>(Resource.Id.ScrollLayout);

            // get Data from DB and make dynamic movieblocks
            MakeBlocks();

            //get movie ID
            movie_ID = Intent.GetStringExtra("MovieID") ?? "Data not available";
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
                this.selectedItem = Convert.ToString(spinner.GetItemAtPosition(e.Position));
                changeView();
                Console.WriteLine("Writeline in the if statement " + this.selectedItem);
            }
            Console.WriteLine(this.selectedItem);
        }

        private void changeView()
        {
            Console.WriteLine("Changeview() " + this.selectedItem);
            // If statement to correctly navigate
            if (this.selectedItem == "Search")
            {
                Console.WriteLine("in de search if statement " + this.selectedItem);
                StartActivity(typeof(Search));
                createSpinner();
            }
            if (this.selectedItem == "Recommended movies")
            {
                Console.WriteLine("in de recommended movies if statement " + this.selectedItem);
                StartActivity(typeof(MainActivity));
                createSpinner();
            }
            if (this.selectedItem == "Login")
            {
                Console.WriteLine("in de login if statement " + this.selectedItem);
                StartActivity(typeof(Login));
                createSpinner();
            }
        }

        public void MakeBlocks()
        {
            HomeViewModel movies = new HomeViewModel();
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
                    // var MoviePage = new Intent(this, typeof(MoviePage));
                    // String Movie_ID = movie.id.ToString();
                    // MoviePage.PutExtra("MovieID", Movie_ID);
                    movie_ID = movie.id;


                    SetContentView(Resource.Layout.MoviePage);
                    // get data from DB and make dynamic movie pages
                    generated_moviePage();

                    // Update method to change star layout + rating
                    update();
                    
                };

                scrollBlock.AddView(movieBlock);
            }
        }

        public void update()
        {
            var rate1 = FindViewById<ImageButton>(Resource.Id.rate1);
            var rate2 = FindViewById<ImageButton>(Resource.Id.rate2);
            var rate3 = FindViewById<ImageButton>(Resource.Id.rate3);
            var rate4 = FindViewById<ImageButton>(Resource.Id.rate4);
            var rate5 = FindViewById<ImageButton>(Resource.Id.rate5);

            //Resets the buttons
            void ResetButton()
            {
                rate1.SetImageResource(Resource.Drawable.emptystar);
                rate2.SetImageResource(Resource.Drawable.emptystar);
                rate3.SetImageResource(Resource.Drawable.emptystar);
                rate4.SetImageResource(Resource.Drawable.emptystar);
                rate5.SetImageResource(Resource.Drawable.emptystar);
            }

            //Each button has to add a rate to a movie and add it to the database
            rate1.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rating = 1;
                }
            };
            rate2.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                    rating = 2;
                }
            };
            rate3.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                    rate3.SetImageResource(Resource.Drawable.fullstar);
                    rating = 3;
                }
            };
            rate4.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                    rate3.SetImageResource(Resource.Drawable.fullstar);
                    rate4.SetImageResource(Resource.Drawable.fullstar);
                    rating = 4;
                }
            };
            rate5.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                    rate3.SetImageResource(Resource.Drawable.fullstar);
                    rate4.SetImageResource(Resource.Drawable.fullstar);
                    rate5.SetImageResource(Resource.Drawable.fullstar);
                    rating = 5;
                }
            };
            Console.WriteLine(rating);
        }

        private void SaveRating(object sender, EventArgs e)
        {
            int id = ((ImageButton)sender).Id;
            String btnId = id.ToString();

            //TextView description = FindViewById<TextView>(Resource.Id.moviePageTitle);
            //description.Text = movie_ID;

        }

        private void UploadRating()
        {


        }

        public void generated_moviePage()
        {
            Console.WriteLine("Moviepage!!");
            MoviePageViewModel MoviePage = new MoviePageViewModel(movie_ID);

            foreach (var page in MoviePage.LoadAllItemFromMySQL())
            {
                Console.WriteLine(page.title);
                TextView moviePageTitle = FindViewById<TextView>(Resource.Id.moviePageTitle);
                moviePageTitle.Text = page.title;

                TextView moviePageRating = FindViewById<TextView>(Resource.Id.moviePageRating);
                moviePageRating.Text = "IMDB rating: " + page.imdb_score;

                TextView duration = FindViewById<TextView>(Resource.Id.duration);
                duration.Text = "Duration: " + page.duration + " min";

                TextView moviePageGenre = FindViewById<TextView>(Resource.Id.moviePageGenre);
                moviePageGenre.Text = page.genres;

                TextView moviePageActors = FindViewById<TextView>(Resource.Id.moviePageActors);
                moviePageActors.Text = "Starring:\n" + page.actor_1_name + "\n" + page.actor_2_name + "\n" + page.actor_3_name;

                TextView moviePageDirector = FindViewById<TextView>(Resource.Id.moviePageDirector);
                moviePageDirector.Text = "Director: \n" + page.director_name;
            }
        }
    }
}



