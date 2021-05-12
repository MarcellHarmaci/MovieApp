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

		/// <summary>
		/// Loads details, seasons and all episodes of the show.
		/// </summary>
		/// <param name="parameter"></param>
		/// <param name="mode"></param>
		/// <param name="state"></param>
		public override async Task OnNavigatedToAsync(
			object parameter,
			NavigationMode mode,
			IDictionary<string, object> state
		)
		{
			var slug = (string)parameter;
			var service = new ShowService();

			// Load show details
			ShowDetails = await service.GetShowDetailsAsync(slug);
			// Load seasons of the show
			var seasonsResult = await service.GetSeasonsOfShowAsync(slug);

			foreach (SeasonDetails season in seasonsResult)
			{
				// Only load episodes of real seasons
				if (season.Number > 0)
				{
					// Load all the episodes of current season
					for (int ep = 1; ep < season.EpisodeCount; ep++)
					{
						var episode = await service.GetEpisodeOfSeasonAsync(ShowDetails.Ids.Slug, season.Number, ep);
						season.Episodes.Add(episode);
					}
				}
					
				// Add loaded season to seasons list
				Seasons.Add(season);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

	}
}
