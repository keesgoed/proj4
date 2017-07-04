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
using Moviestar.Droid.ViewModels;


namespace Moviestar.Droid
{
    [Activity(Label = "Movie Page")]
    public class MoviePage : Activity
    {
        String selectedItem;
        String movie_ID;
        int rating = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MoviePage);

            movie_ID = Intent.GetStringExtra("MovieID");

            //Create object of MovieList and create spinner
            createSpinner();

            // get data from DB and make dynamic movie pages
            generated_moviePage();

            // Update method to change star layout + rating
            update();

            //Search functionality
            CreateSearchBar();

            var rateButton = FindViewById<Button>(Resource.Id.rateMovie);
            rateButton.Click += delegate
            {
                uploadRating();
                StartActivity(typeof(RatedMovies));
            };

        }

        public void generated_moviePage()
        {
            MoviePageViewModel MoviePage = new MoviePageViewModel(movie_ID);

            foreach (var page in MoviePage.LoadAllItemFromMySQL())
            {
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
                Console.WriteLine("Dit is de rating: " + rating);
            }

            //Each button has to add a rate to a movie and add it to the database
            rate1.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                }
                rating = 1;
            };
            rate2.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                }
                rating = 2;
            };
            rate3.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                    rate3.SetImageResource(Resource.Drawable.fullstar);
                }
                rating = 3;
            };
            rate4.Touch += (object sender, View.TouchEventArgs e) => {
                ResetButton();
                if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Up)
                {
                    rate1.SetImageResource(Resource.Drawable.fullstar);
                    rate2.SetImageResource(Resource.Drawable.fullstar);
                    rate3.SetImageResource(Resource.Drawable.fullstar);
                    rate4.SetImageResource(Resource.Drawable.fullstar);
                }
                rating = 4;
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
                }
                rating = 5;
            };
        }


        public void uploadRating()
        {
            RatingViewModel MovieRating = new RatingViewModel("1", this.movie_ID, this.rating);
            MovieRating.LoadAllItemFromMySQL();
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