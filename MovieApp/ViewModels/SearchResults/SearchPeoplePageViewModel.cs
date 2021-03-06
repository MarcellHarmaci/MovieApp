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
	class SearchPeoplePageViewModel : ViewModelBase
	{

		public ObservableCollection<Person> People { get; set; } =
			new ObservableCollection<Person>();

		/// <summary>
		/// No result text is only visible when the list of people is empty
		/// </summary>
		public Visibility NoResultTextVisibility
		{
			get
			{
				if (People.Count > 0)
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

			// Get search result
			var service = new PersonService();
			var result = await service.GetPersonSearchResultsAsync(searchTerm);

			foreach (Person person in result)
			{
				// Display new reuslt
				People.Add(person);
				// Update no result text visibility
				RaisePropertyChanged(() => NoResultTextVisibility);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToPersonDetails(string slug)
		{
			NavigationService.Navigate(typeof(PersonDetailsPage), slug);
		}

	}
}
