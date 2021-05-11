using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace MovieApp.ViewModels.Details
{
	class PersonDetailsPageViewModel : ViewModelBase
	{

		private PersonDetails personDetails;
		public PersonDetails PersonDetails
		{
			get => personDetails;
			set
			{
				personDetails = value;
				RaisePropertyChanged(() => PersonDetails);
			}
		}

		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		)
		{
			var slug = (string)parameter;
			var service = new PersonService();
			PersonDetails = await service.GetPersonDetailsAsync(slug);

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

	}
}
