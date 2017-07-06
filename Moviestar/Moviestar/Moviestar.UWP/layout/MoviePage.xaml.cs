using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Moviestar.Models;
using Moviestar.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Moviestar.UWP.layout
{
    public sealed partial class MoviePage : Page
    {
        public List<Movie> SelectedMovie;
        string item;

        public MoviePage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            List<Movie> movie = e.Parameter as List<Movie>;
            if(movie != null)
            {
                SelectedMovie = movie;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MovieList));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RatedMovies));
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search), SearchBox.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            this.item = item.Content.ToString();

            if (this.item == "Rate 1 star")
                this.item = "1";
            if (this.item == "Rate 2 stars")
                this.item = "2";
            if (this.item == "Rate 3 stars")
                this.item = "3";
            if (this.item == "Rate 4 stars")
                this.item = "4";
            if (this.item == "Rate 5 stars")
                this.item = "5";

        }

        private void Button_Rate_Movie(object sender, RoutedEventArgs e)
        {
            MoviePageViewModel MovieRating = new MoviePageViewModel();
            MovieRating.RateMovie("1", SelectedMovie[0].id , Int32.Parse(this.item));
        }
    }
}
