using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels
{
	class MovieDetailsPageViewModel : ViewModelBase
	{
		private MovieDetails movieDetails;
		public MovieDetails MovieDetails {
			get => movieDetails;
			set {
				movieDetails = value;
				RaisePropertyChanged(() => MovieDetails);
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
			RaisePropertyChanged(() => MovieDetails);

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

	}
}
