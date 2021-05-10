using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels
{
	class SearchShowsPageViewModel : ViewModelBase
	{

		public ObservableCollection<Show> Shows { get; set; } =
			new ObservableCollection<Show>();

		private string searchTerm = "<Search term>";
		public string SearchTerm
		{
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
			var service = new MovieService();
			var result = await service.GetShowSearchResultsAsync(searchTerm);

			Shows.Clear();
			foreach (Show show in result)
			{
				Shows.Add(show);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToShowDetails(string slug)
		{
			//NavigationService.Navigate(typeof(ShowDetailsPage), slug);
		}

	}
}
