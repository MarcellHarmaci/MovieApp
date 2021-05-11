using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
	class TraktService
	{
		protected readonly string traktApiKey;
		protected readonly Uri serverUrl;

		public TraktService()
		{
			if (Windows.UI.Core.CoreWindow.GetForCurrentThread() != null)
			{
				var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
				traktApiKey = resourceLoader.GetString("TraktApiKey");
				serverUrl = new Uri(resourceLoader.GetString("TraktServerUri"));
			}
		}

		protected async Task<T> GetAsync<T>(Uri uri)
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
					// Run GET method
					var response = await httpClient.GetAsync(uri);
					response.EnsureSuccessStatusCode();

					// Convert json result
					json = await response.Content.ReadAsStringAsync();
					T result = JsonConvert.DeserializeObject<T>(json);

					return result;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine($"Exception: {ex.StackTrace}\nMessage: {ex.Message}");
				}

				return default(T);
			}
		}

	}
}
