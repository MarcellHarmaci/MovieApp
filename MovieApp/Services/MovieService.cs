using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using MovieApp.Models;
using System.Collections.ObjectModel;

namespace MovieApp.Services
{
	class MovieService
	{

		private readonly Uri serverUrl = new Uri("https://api.trakt.tv/");
		private readonly string traktApiKey = "8df54ee23f5b0bcb8bd85b72c49444b2bab83a8d13979be03ff2f9fcfc04c56b";

		private async Task<T> GetAsync<T>(Uri uri)
		{
			using (var httpClient = new HttpClient())
			{
				// Add header to the GET request 
				HttpRequestHeaders headers = httpClient.DefaultRequestHeaders;
				headers.Add("ContentType", "application/json");
				headers.Add("trakt-api-version", "2");
				headers.Add("trakt-api-key", traktApiKey);

				string json = "";

				try
				{
					var response = await httpClient.GetAsync(uri);
					response.EnsureSuccessStatusCode();
					json = await response.Content.ReadAsStringAsync();
					T result = JsonConvert.DeserializeObject<T>(json);
					return result;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine($"Exception: {ex.StackTrace}\nMessage: {ex.Message}");
				}
				System.Diagnostics.Debug.WriteLine(json);

				return default(T);
			}
		}

		public async Task<List<Movie>> GetPopularMoviesAsync(
			bool withPagination = false,
			int? page = null,
			int? limit = null
		)
		{
			string relativeUri = "movies/popular";

			if (withPagination && page != null && limit != null)
				relativeUri += $"?page={page}&limit={limit}";

			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<List<Movie>>(uri);
		}

		public async Task<MovieDetails> GetMovieDetailsAsync(string slug)
		{
			string relativeUri = $"movies/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<MovieDetails>(uri);
		}

		public async Task<ShowDetails> GetShowDetailsAsync(string slug)
		{
			string relativeUri = $"shows/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<ShowDetails>(uri);
		}

		public async Task<PersonDetails> GetPersonDetailsAsync(string slug)
		{
			string relativeUri = $"people/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<PersonDetails>(uri);
		}

		public async Task<List<Movie>> GetMovieSearchResultsAsync(string searchTerm)
		{
			string relativeUri = $"search/movie?query=*{searchTerm}*";
			Uri uri = new Uri(serverUrl, relativeUri);
			var searchResults = await GetAsync<List<MovieSearchResult>>(uri);

			var movies = new List<Movie>();
			if (searchResults != null && searchResults.Count != 0)
			{
				foreach (MovieSearchResult result in searchResults)
				{
					movies.Add(result.Movie);
				}
			}

			return movies;
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

		public async Task<List<Person>> GetPersonSearchResultsAsync(string searchTerm)
		{
			string relativeUri = $"search/person?query={searchTerm}*";
			Uri uri = new Uri(serverUrl, relativeUri);
			var searchResults = await GetAsync<List<PersonSearchResult>>(uri);

			var people = new List<Person>();
			if (searchResults != null && searchResults.Count != 0)
			{
				foreach (PersonSearchResult result in searchResults)
				{
					people.Add(result.Person);
				}
			}

			return people;
		}

		public async Task<MovieDetails> GetSimilarMoviesAsync(string slug)
		{
			string relativeUri = $"movies/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<MovieDetails>(uri);
		}

		public async Task<Cast[]> GetPeopleOfMovieAsync(string slug)
		{
			System.Diagnostics.Debug.WriteLine(slug);

			string relativeUri = $"movies/{slug}/people";
			Uri uri = new Uri(serverUrl, relativeUri);

			System.Diagnostics.Debug.WriteLine(uri);

			var staff = await GetAsync<ProductionStaff>(uri);

			System.Diagnostics.Debug.WriteLine(staff);

			return staff.Cast;
		}

		public async Task<ProductionStaff> GetPeopleOfShowAsync(string slug)
		{
			string relativeUri = $"shows/{slug}/poeple";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<ProductionStaff>(uri);
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
