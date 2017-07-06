using Moviestar.Models;
using Moviestar.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Moviestar.UWP.layout
{
    public sealed partial class Search : Page
    {
        List<Movie> Movies;
        String Query;
        public Search()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string searchquery = e.Parameter as string;
            if (searchquery != null)
            {
                Query = searchquery;
                SearchViewModel searchlist = new SearchViewModel();
                Movies = searchlist.GetMovies(Query);
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

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var movie = (Movie)e.ClickedItem;
            List<Movie> movielist = new List<Movie>();
            movielist.Add(movie);
            Frame.Navigate(typeof(MoviePage), movielist);
        }

    }
}
