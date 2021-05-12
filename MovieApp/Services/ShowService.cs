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
		/// <summary>
		/// Get details of a show by it's slug.
		/// </summary>
		/// <param name="slug">Show slug</param>
		/// <returns>Details of the given show</returns>
		public async Task<ShowDetails> GetShowDetailsAsync(string slug)
		{
			string relativeUri = $"shows/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<ShowDetails>(uri);
		}

		/// <summary>
		/// Search for shows by their title.
		/// </summary>
		/// <param name="searchTerm">Search term - A shows's title or part of it</param>
		/// <returns>List of shows that match search criteria</returns>
		public async Task<List<Show>> GetShowSearchResultsAsync(string searchTerm)
		{
			string relativeUri = $"search/show?query=*{searchTerm}*";
			Uri uri = new Uri(serverUrl, relativeUri);

			// Get search results
			var searchResults = await GetAsync<List<ShowSearchResult>>(uri);

			// Convert search results to shows
			var shows = new List<Show>();
			if (searchResults != null && searchResults.Count != 0)
			{
				// Get show object out of search result
				foreach (ShowSearchResult result in searchResults)
				{
					shows.Add(result.Show);
				}
			}

			return shows;
		}

		/// <summary>
		/// Gets all the seasons of a show in detail by it's slug.
		/// </summary>
		/// <param name="slug">Show's slug</param>
		/// <returns>List of detailed seasons of the show</returns>
		public async Task<List<SeasonDetails>> GetSeasonsOfShowAsync(string slug)
		{
			string relativeUri = $"shows/{slug}/seasons?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);

			return await GetAsync<List<SeasonDetails>>(uri);
		}

		/// <summary>
		/// Gets an episode of a given show's season.
		/// </summary>
		/// <param name="slug">Show's slug to get an episode of</param>
		/// <param name="season">Number of the season to get an episode of</param>
		/// <param name="episode">Number of episode to get</param>
		/// <returns>Desired episode of the show</returns>
		public async Task<Episode> GetEpisodeOfSeasonAsync(string slug, int season, int episode)
		{
			string relativeUri = $"shows/{slug}/seasons/{season}/episodes/{episode}";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<Episode>(uri);
		}

	}
}
