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
using MovieApp.Views;

namespace MovieApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public ObservableCollection<Movie> PopularMovies { get; set; } =
			new ObservableCollection<Movie>();

		public string SearchTerm { get; set; }

		public int currentPage = 1;
		public int CurrentPage
		{
			get => currentPage;
			set
			{
				if (value > 0 && value <= 10)
				{
					currentPage = value;
					RaisePropertyChanged(() => this.IsPrevPageAvailable);
					RaisePropertyChanged(() => this.IsNextPageAvailable);
					RaisePropertyChanged(() => this.PagingString);
				}
			}
		}
		
		private readonly int PageLimit = 10;

		public string PagingString
		{
			get { return $"{CurrentPage} / {PageLimit}"; }
		}

		public bool IsPrevPageAvailable
		{
			get { return CurrentPage > 1; }
		}

		public bool IsNextPageAvailable
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
			var movies = await service.GetPopularMoviesAsync(true, CurrentPage, PageLimit);

			foreach (var item in movies)
			{
				PopularMovies.Add(item);
			}
		}

		public async Task PrevPage()
		{
			if (CurrentPage > 1)
			{
				CurrentPage--;
				await LoadCurrentPage();
			}
		}

		public async Task NextPage()
		{
			if (CurrentPage < 10)
			{
				CurrentPage++;
				await LoadCurrentPage();
			}
		}

		public void Search()
		{
			System.Diagnostics.Debug.WriteLine($"Searching by term: {SearchTerm}");
		}

		internal void NavigateToMovieDetails(string movieSlug)
		{
			NavigationService.Navigate(typeof(MovieDetailsPage), movieSlug);
		}

	}
}

