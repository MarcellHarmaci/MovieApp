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
				RaisePropertyChanged(() => MovieLength);
			}
		}

		public string MovieLength { 
			get {
				try
				{
					return $"{MovieDetails.Runtime} minutes";
				}
				catch(NullReferenceException ex)
				{
					return "<number>";
				}
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

		//public async Task OpenTrailer()
		//{
		//	// The URI to launch
		//	var uriBing = new Uri(MovieDetails.Trailer.ToString());

		//	// Launch the URI
		//	await Windows.System.Launcher.LaunchUriAsync(uriBing);
		//}
	}
}
