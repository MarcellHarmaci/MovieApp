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
	class ShowDetailsPageViewModel : ViewModelBase
	{

		private ShowDetails showDetails;
		public ShowDetails ShowDetails
		{
			get => showDetails;
			set
			{
				showDetails = value;
				RaisePropertyChanged(() => ShowDetails);
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
			ShowDetails = await service.GetShowDetailsAsync(slug);

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

	}
}
