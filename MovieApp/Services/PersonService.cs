using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
	class PersonService : TraktService
	{
		/// <summary>
		/// Gets a person's detauls by their Trakt slug
		/// </summary>
		/// <param name="slug">Person's Trakt slug</param>
		/// <returns>Details of given person</returns>
		public async Task<PersonDetails> GetPersonDetailsAsync(string slug)
		{
			string relativeUri = $"people/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<PersonDetails>(uri);
		}

		/// <summary>
		/// Search for people by their name
		/// </summary>
		/// <param name="searchTerm">Search term - A person's name or part of it</param>
		/// <returns>List of people that match search criteria</returns>
		public async Task<List<Person>> GetPersonSearchResultsAsync(string searchTerm)
		{
			string relativeUri = $"search/person?query={searchTerm}*";
			Uri uri = new Uri(serverUrl, relativeUri);
			
			// Get search results
			var searchResults = await GetAsync<List<PersonSearchResult>>(uri);

			// Convert search results to people
			var people = new List<Person>();
			if (searchResults != null && searchResults.Count != 0)
			{
				// Get Person object out of search result
				foreach (PersonSearchResult result in searchResults)
				{
					people.Add(result.Person);
				}
			}

			return people;
		}

		/// <summary>
		/// Get cast of a movie by movie slug
		/// </summary>
		/// <param name="slug">Movie slug</param>
		/// <returns>The cast of the given movie.</returns>
		public async Task<Cast[]> GetCastOfMovieAsync(string slug)
		{
			string relativeUri = $"movies/{slug}/people";
			Uri uri = new Uri(serverUrl, relativeUri);

			var staff = await GetAsync<ProductionStaff>(uri);
			return staff.Cast;
		}

	}
}
