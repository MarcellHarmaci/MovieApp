using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using MovieApp.Models;

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
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
				}
				System.Diagnostics.Debug.WriteLine(json);

				T result = JsonConvert.DeserializeObject<T>(json);
				return result;
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

	}
}
