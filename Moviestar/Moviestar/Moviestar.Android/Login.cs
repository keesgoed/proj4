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
    [Activity(Label = "Login")]
    class Login : Activity
    {
        String selectedItem;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            //Create navigation
            createSpinner();

            //Search functionality
            CreateSearchBar();

            var loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            loginBtn.Click += delegate
            {
                checkCredentials();
            };

        }

        public void checkCredentials()
        {
            var username = FindViewById<TextView>(Resource.Id.usernameInput);
            var password = FindViewById<TextView>(Resource.Id.passwordInput);

            if (username.Text != "" && password.Text != "")
            {
                var loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
                loginBtn.Text = "wow!";

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
                createSpinner();
            };

        }
    }
}