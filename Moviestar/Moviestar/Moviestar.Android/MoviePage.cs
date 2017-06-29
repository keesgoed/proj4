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

namespace Moviestar.Droid
{
    [Activity(Label = "MoviePage")]
    public class MoviePage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MoviePage);

            // Create your application here

            Button MoviePageBackButton = FindViewById<Button>(Resource.Id.moviePageBackButton);
            MoviePageBackButton.Click += delegate {
                StartActivity(typeof(MainActivity));
            };

            ImageButton starbutton1 = FindViewById<ImageButton>(Resource.Id.star1MoviePage);
            ImageButton starbutton2 = FindViewById<ImageButton>(Resource.Id.star2MoviePage);
            ImageButton starbutton3 = FindViewById<ImageButton>(Resource.Id.star3MoviePage);
            ImageButton starbutton4 = FindViewById<ImageButton>(Resource.Id.star4MoviePage);
            ImageButton starbutton5 = FindViewById<ImageButton>(Resource.Id.star5MoviePage);
            starbutton1.Click += SaveRating;
            starbutton2.Click += SaveRating;
            starbutton3.Click += SaveRating;
            starbutton4.Click += SaveRating;
            starbutton5.Click += SaveRating;
        }

        private void SaveRating(object sender, EventArgs e)
        {
            int id = ((ImageButton)sender).Id;
            String btnId = id.ToString();

            TextView description = FindViewById<TextView>(Resource.Id.descriptionMoviePage);
            description.Text = btnId;

        }
    }
}