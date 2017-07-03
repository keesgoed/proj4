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
using Moviestar.Droid.ViewModels;


namespace Moviestar.Droid
{
    [Activity(Label = "MoviePage")]
    public class MoviePage : Activity
    {
        String selected_item;
        String movie_ID;
        int rating;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MoviePage);

            movie_ID = Intent.GetStringExtra("MovieID");

            // get data from DB and make dynamic movie pages
            generated_moviePage();

            // Update method to change star layout + rating
            update();

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
    }
}