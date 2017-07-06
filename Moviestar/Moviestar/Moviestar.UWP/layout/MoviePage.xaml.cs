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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Moviestar.UWP.layout
{
    public sealed partial class MoviePage : Page
    {
        public List<Movie> SelectedMovie;

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

    }
}
