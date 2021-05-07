using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using MovieApp.Models;
using System.Collections.ObjectModel;
using MovieApp.Services;

namespace MovieApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public string Response { get; set; } = "";

		public ObservableCollection<Movie> PopularMovies { get; set; } =
			new ObservableCollection<Movie>();

		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		) {
			var service = new MovieService();
			var movies = await service.GetPopularMoviesAsync();

			foreach (var item in movies)
			{
				PopularMovies.Add(item);
				System.Diagnostics.Debug.WriteLine(item.title);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}
	}
}

