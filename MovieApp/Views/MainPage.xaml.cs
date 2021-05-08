using MovieApp.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MovieApp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Movies_ItemClick(object sender, ItemClickEventArgs e)
        {
            // TODO Navigate to movie details page
        }

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
            ViewModel.Search();
		}

        private void TextBox_Search_OnKeyDown(object sender, KeyRoutedEventArgs e)
		{
            if(e.Key == Windows.System.VirtualKey.Enter)
			{
                ViewModel.Search();
            }
		}

        private void PopularMovies_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var movie = (Movie)e.ClickedItem;
            ViewModel.NavigateToMovieDetails(movie.Ids.slug);
        }

    }
}
