using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
	class ShowService : TraktService
	{

		public async Task<ShowDetails> GetShowDetailsAsync(string slug)
		{
			string relativeUri = $"shows/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<ShowDetails>(uri);
		}

		public async Task<List<Show>> GetShowSearchResultsAsync(string searchTerm)
		{
			string relativeUri = $"search/show?query=*{searchTerm}*";
			Uri uri = new Uri(serverUrl, relativeUri);
			var searchResults = await GetAsync<List<ShowSearchResult>>(uri);

			var shows = new List<Show>();
			if (searchResults != null && searchResults.Count != 0)
			{
				foreach (ShowSearchResult result in searchResults)
				{
					shows.Add(result.Show);
				}
			}

			return shows;
		}

		public async Task<List<SeasonDetails>> GetSeasonsOfShowAsync(string slug)
		{
			string relativeUri = $"shows/{slug}/seasons?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);

			return await GetAsync<List<SeasonDetails>>(uri);
		}

		public async Task<Episode> GetEpisodesOfSeasonAsync(string slug, int season, int episode)
		{
			string relativeUri = $"shows/{slug}/seasons/{season}/episodes/{episode}";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<Episode>(uri);
		}

	}
}
