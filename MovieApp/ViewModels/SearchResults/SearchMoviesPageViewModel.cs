using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Views.Details;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels.SearchResults
{
	class SearchMoviesPageViewModel : ViewModelBase
	{
		
		public ObservableCollection<Movie> Movies { get; set; } =
			new ObservableCollection<Movie>();

		/// <summary>
		/// No result text is only visible when the list of movies is empty
		/// </summary>
		public Visibility NoResultTextVisibility
		{
			get
			{
				if (Movies.Count > 0)
					return Visibility.Collapsed;
				else
					return Visibility.Visible;
			}
		}

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

			// Get search results
			var service = new MovieService();
			var result = await service.GetMovieSearchResultsAsync(searchTerm);

			foreach(Movie movie in result)
			{
				// Display new result
				Movies.Add(movie);
				// Update no result text visibility
				RaisePropertyChanged(() => NoResultTextVisibility);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToMovieDetails(string slug)
		{
			NavigationService.Navigate(typeof(MovieDetailsPage), slug);
		}
	}
}
