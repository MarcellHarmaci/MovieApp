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

namespace MovieApp.ViewModels.SearchResults
{
	class SearchPeoplePageViewModel : ViewModelBase
	{

		public ObservableCollection<Person> People { get; set; } =
			new ObservableCollection<Person>();

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
			var service = new PersonService();
			var result = await service.GetPersonSearchResultsAsync(searchTerm);

			People.Clear();
			foreach (Person person in result)
			{
				People.Add(person);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		internal void NavigateToPersonDetails(string slug)
		{
			NavigationService.Navigate(typeof(PersonDetailsPage), slug);
		}

	}
}
