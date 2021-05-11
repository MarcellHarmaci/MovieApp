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

		public async Task<PersonDetails> GetPersonDetailsAsync(string slug)
		{
			string relativeUri = $"people/{slug}?extended=full";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<PersonDetails>(uri);
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

		public async Task<Cast[]> GetCastOfMovieAsync(string slug)
		{
			string relativeUri = $"movies/{slug}/people";
			Uri uri = new Uri(serverUrl, relativeUri);

			System.Diagnostics.Debug.WriteLine(uri);

			var staff = await GetAsync<ProductionStaff>(uri);

			return staff.Cast;
		}

	}
}
