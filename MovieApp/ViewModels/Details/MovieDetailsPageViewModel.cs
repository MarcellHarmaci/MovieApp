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
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels.Details
{
	class MovieDetailsPageViewModel : ViewModelBase
	{
		private MovieDetails movieDetails;
		public MovieDetails MovieDetails {
			get => movieDetails;
			set {
				movieDetails = value;
				RaisePropertyChanged(() => MovieDetails);
				RaisePropertyChanged(() => MovieLength);
			}
		}

		private Cast[] cast;
		public Cast[] Cast
		{
			get => cast;
			set
			{
				cast = value;
				RaisePropertyChanged(() => Cast);
			}
		}

		public string MovieLength { 
			get {
				if (MovieDetails != null)
					return $"{MovieDetails.Runtime} minutes";
				else
					return "<length>";
			}
		}

		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		)
		{
			// Get slug of movie to display
			var slug = (string)parameter;

			// Instantiate required services
			var movieService = new MovieService();
			var personService = new PersonService();
			
			// Get details of current movie
			MovieDetails = await movieService.GetMovieDetailsAsync(slug);
			// Get the cast of current movie
			Cast = await personService.GetCastOfMovieAsync(slug);

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToPersonDetails(string slug)
		{
			NavigationService.Navigate(typeof(PersonDetailsPage), slug);
		}

	}
}
