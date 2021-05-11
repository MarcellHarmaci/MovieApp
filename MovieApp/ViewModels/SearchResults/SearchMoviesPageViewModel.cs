using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels.SearchResults
{
	class SearchMoviesPageViewModel : ViewModelBase
	{
		
		public ObservableCollection<Movie> Movies { get; set; } =
			new ObservableCollection<Movie>();

		private string searchTerm = "<Search term>";
		public string SearchTerm {
			get => $"Search results for \"{searchTerm}\""; 
			set
			{
				searchTerm = value;
				RaisePropertyChanged(() => SearchTerm);
			}
		}

		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		)
		{
			SearchTerm = (string)parameter;
			var service = new MovieService();
			var result = await service.GetMovieSearchResultsAsync(searchTerm);

			Movies.Clear();
			foreach(Movie movie in result)
			{
				Movies.Add(movie);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToMovieDetails(string slug)
		{
			NavigationService.Navigate(typeof(MovieDetailsPage), slug);
		}
	}
}
