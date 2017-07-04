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
    class Nav : Activity
    {
        String selectedItem;
    
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
    }
}