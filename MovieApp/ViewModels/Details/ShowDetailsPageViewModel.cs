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

namespace MovieApp.ViewModels.Details
{
	class ShowDetailsPageViewModel : ViewModelBase
	{

		public ObservableCollection<SeasonDetails> Seasons { get; set; } =
			new ObservableCollection<SeasonDetails>();

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
			var seasonsResult = await service.GetSeasonsOfShowAsync(slug);

			foreach(SeasonDetails season in seasonsResult)
			{
				for (int ep = 1; ep < season.EpisodeCount; ep++)
				{
					var episode = await service.GetEpisodesOfSeasonAsync(ShowDetails.Ids.Slug, season.Number, ep);
					season.Episodes.Add(episode);
				}
				Seasons.Add(season);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

	}
}
