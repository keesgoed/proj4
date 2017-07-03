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


    }
}