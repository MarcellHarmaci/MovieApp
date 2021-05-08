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
using System.ComponentModel;

namespace MovieApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public ObservableCollection<Movie> PopularMovies { get; set; } =
			new ObservableCollection<Movie>();

		public string SearchTerm { get; set; }

		public int currentPage;
		public int CurrentPage
		{
			get => currentPage;
			set
			{
				if (value > 0 && value <= 10)
				{
					currentPage = value;
					RaisePropertyChanged(() => this.isPrevPageAvailable);
					RaisePropertyChanged(() => this.isNextPageAvailable);
				}
			}
		}
		private readonly int pageLimit = 10;

		public bool isPrevPageAvailable
		{
			get { return CurrentPage > 1; }
		}

		public bool isNextPageAvailable
		{
			get { return CurrentPage < 10; }
		}

		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		)
		{
			await LoadCurrentPage();

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		public async Task LoadCurrentPage()
		{
			PopularMovies.Clear();

			var service = new MovieService();
			var movies = await service.GetPopularMoviesAsync(true, CurrentPage, pageLimit);

			foreach (var item in movies)
			{
				PopularMovies.Add(item);
			}
		}

		public async Task PrevPage()
		{
			CurrentPage--;
			await LoadCurrentPage();
		}

		public async Task NextPage()
		{
			CurrentPage++;
			await LoadCurrentPage();
		}

		public void Search()
		{
			System.Diagnostics.Debug.WriteLine($"Searching by term: {SearchTerm}");
		}
	}
}

