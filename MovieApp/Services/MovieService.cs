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
	class MovieService : TraktService
	{

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

	}
}
