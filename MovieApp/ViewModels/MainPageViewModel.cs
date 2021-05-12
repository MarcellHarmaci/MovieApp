using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Views.Details;
using MovieApp.Views.SearchResults;

namespace MovieApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public ObservableCollection<Movie> PopularMovies { get; set; } =
			new ObservableCollection<Movie>();

		public ObservableCollection<string> GenreNames { get; set; } =
			new ObservableCollection<string>();

		private List<Genre> Genres = new List<Genre>();

		public int selectedGenreIndex = 0;
		public int SelectedGenreIndex 
		{
			get => selectedGenreIndex;
			set
			{
				selectedGenreIndex = value;
				_ = LoadCurrentPage();
			}
		}

		public string SearchCategory { get; set; }
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
					RaisePropertyChanged(() => IsPrevPageAvailable);
					RaisePropertyChanged(() => IsNextPageAvailable);
					RaisePropertyChanged(() => PagingString);
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
			get { return CurrentPage < PageLimit; }
		}

		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		)
		{
			// Load available genres
			var genreService = new GenreService();
			Genres = await genreService.GetMovieGenresAsync();

			foreach (Genre genre in Genres)
			{
				GenreNames.Add(genre.Name);
			}

			// Load popular movies of first available genre
			await LoadCurrentPage();

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		/// <summary>
		/// Loads popular movies in selected genre for the current page number.
		/// </summary>
		public async Task LoadCurrentPage()
		{
			var service = new MovieService();
			var movies = await service.GetPopularMoviesByGenreAsync(Genres[SelectedGenreIndex].Slug, true, CurrentPage, PageLimit);

			PopularMovies.Clear();
			foreach (var item in movies)
			{
				PopularMovies.Add(item);
			}
		}

		/// <summary>
		/// Decrements current page selected if possible and reloads page accordingly.
		/// </summary>
		public async Task PrevPage()
		{
			if (CurrentPage > 1)
			{
				CurrentPage--;
				await LoadCurrentPage();
			}
		}

		/// <summary>
		/// Increments current page selected if possible and reloads page accordingly.
		/// </summary>
		public async Task NextPage()
		{
			if (CurrentPage < PageLimit)
			{
				CurrentPage++;
				await LoadCurrentPage();
			}
		}

		/// <summary>
		/// Searches for current search term in selected
		/// </summary>
		public void Search()
		{
			if (ValidateString(SearchCategory) && ValidateString(SearchTerm))
			{
				switch (SearchCategory)
				{
					case "Movies":
						{
							NavigationService.Navigate(typeof(SearchMoviesPage), SearchTerm);
							break;
						}
					case "Shows":
						{
							NavigationService.Navigate(typeof(SearchShowsPage), SearchTerm);
							break;
						}
					case "People":
						{
							NavigationService.Navigate(typeof(SearchPeoplePage), SearchTerm);
							break;
						}
					default:
						{
							System.Diagnostics.Debug.WriteLine($"Cannot search by term {SearchTerm} in {SearchCategory}");
							break;
						}
				}
			}
			else System.Diagnostics.Debug.WriteLine($"Invalid state to search");
		}

		private bool ValidateString(string value)
		{
			return value != null && value != "";
		}

		/// <summary>
		/// Open movie details page for clicked movie
		/// </summary>
		/// <param name="movieSlug">Slug of movie</param>
		internal void NavigateToMovieDetails(string movieSlug)
		{
			NavigationService.Navigate(typeof(MovieDetailsPage), movieSlug);
		}

	}

}

