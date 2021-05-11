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

		public ObservableCollection<ProductionStaff> Staff { get; set; } =
			new ObservableCollection<ProductionStaff>();

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
			Cast = await service.GetPeopleOfMovieAsync(slug);

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToPersonDetails(string slug)
		{
			NavigationService.Navigate(typeof(PersonDetailsPage), slug);
		}

	}
}
