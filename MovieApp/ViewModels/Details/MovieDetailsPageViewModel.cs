using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
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
			var slug = (string)parameter;
			var service = new MovieService();
			MovieDetails = await service.GetMovieDetailsAsync(slug);

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

	}
}
