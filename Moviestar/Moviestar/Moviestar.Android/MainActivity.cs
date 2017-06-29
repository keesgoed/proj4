using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Moviestar.Droid
{
    [Activity(Label = "Moviestar.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        String selectedItem = "Recommended movies";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            //SetContentView (Resource.Layout.Login);

            SetContentView(Resource.Layout.Main);
            update1();

            // Get our button from the layout resource,
            // and attach an event to it

            //create spinner

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.menu_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

        }

        public void update1()
        {
            Button MoviePageButton = FindViewById<Button>(Resource.Id.moviePageButton);
            MoviePageButton.Click += MoviePageButton_Click;

        }

        public void update2()
        {
            Button MoviePageBackButton = FindViewById<Button>(Resource.Id.moviePageBackButton);
            MoviePageBackButton.Click += MoviePageBackButton_Click;
        }

        private void MoviePageButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.MoviePage);
            update2();
        }

        private void MoviePageBackButton_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.Main);
            update1();
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
                SetContentView(Resource.Layout.Main);
            }
            if (this.selectedItem == "Login")
            {
                //  SetContentView(Resource.Layout.Login);
            }
        }
    }
}



