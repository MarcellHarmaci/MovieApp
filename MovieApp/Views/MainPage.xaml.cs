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
            ViewModel.NavigateToMovieDetails(movie.Ids.Slug);
        }

        private void ComboBox_SelectionChaged(object sender, SelectionChangedEventArgs e)
        {
            string searchCategory = e.AddedItems[0].ToString();
            ViewModel.SearchCategory = searchCategory;
        }

    }
}
