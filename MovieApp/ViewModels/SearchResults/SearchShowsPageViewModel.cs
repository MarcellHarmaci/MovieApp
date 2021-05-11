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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels.SearchResults
{
	class SearchShowsPageViewModel : ViewModelBase
	{

		public ObservableCollection<Show> Shows { get; set; } =
			new ObservableCollection<Show>();

		public Visibility NoResultTextVisibility
		{
			get
			{
				if (Shows.Count > 0)
					return Visibility.Collapsed;
				else
					return Visibility.Visible;
			}
		}

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
			var service = new ShowService();
			var result = await service.GetShowSearchResultsAsync(searchTerm);

			Shows.Clear();
			foreach (Show show in result)
			{
				Shows.Add(show);
				RaisePropertyChanged(() => NoResultTextVisibility);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToShowDetails(string slug)
		{
			NavigationService.Navigate(typeof(ShowDetailsPage), slug);
		}

	}
}
