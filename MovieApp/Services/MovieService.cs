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
		/// <summary>
		/// Gets popular movies in a given renre.
		/// </summary>
		/// <param name="genreSlug">Slug of genre to query. Can be Irakt slug or Imdb ID</param>
		/// <param name="withPagination">Weather or not to query with paging</param>
		/// <param name="page">Desired page</param>
		/// <param name="limit">Number of movies per page</param>
		/// <returns>List of movies in given genre (and page if paging is enabled).</returns>
		public async Task<List<Movie>> GetPopularMoviesByGenreAsync(
			string genreSlug,
			bool withPagination = false,
			int? page = null,
			int? limit = null
		)
		{
			string relativeUri = $"movies/popular?genres={genreSlug}";

			if (withPagination && page != null && limit != null)
				relativeUri += $"&page={page}&limit={limit}";

			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<List<Movie>>(uri);
		}

		/// <summary>
		/// Gets all details of a movie.
		/// </summary>
		/// <param name="slug">Movie slug</param>
		/// <returns>Details of desired movie</returns>
		public async Task<MovieDetails> GetMovieDetailsAsync(string slug)
		{
			string relativeUri = $"movies/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<MovieDetails>(uri);
		}

		/// <summary>
		/// Gets movie search results for a given search term.
		/// </summary>
		/// <param name="searchTerm">Search term to query movies by</param>
		/// <returns>List of movies by Trakt for given search term</returns>
		public async Task<List<Movie>> GetMovieSearchResultsAsync(string searchTerm)
		{
			string relativeUri = $"search/movie?query={searchTerm}*";
			Uri uri = new Uri(serverUrl, relativeUri);

			// Get search results from Trakt API
			var searchResults = await GetAsync<List<MovieSearchResult>>(uri);

			// Convert search results to a Movie list
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
