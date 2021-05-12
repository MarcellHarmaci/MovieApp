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
	abstract class TraktService
	{
		/// <summary>
		/// Trakt API key
		/// </summary>
		protected readonly string traktApiKey;
		/// <summary>
		/// Trakt server base URL
		/// </summary>
		protected readonly Uri serverUrl;

		/// <summary>
		/// Initializes Trakt API key and server url from string resources.<br/>
		/// String resoure names used: 
		/// <list type="bullet">
		/// <item>"TraktApiKey"</item>
		/// <item>"TraktServerUri"</item>
		/// </list>
		/// </summary>
		public TraktService()
		{
			if (Windows.UI.Core.CoreWindow.GetForCurrentThread() != null)
			{
				var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
				traktApiKey = resourceLoader.GetString("TraktApiKey");
				serverUrl = new Uri(resourceLoader.GetString("TraktServerUri"));
			}
		}

		/// <summary>
		/// Trakt Api GET method template.
		/// Sets the necessary request headers and parses response json.
		/// </summary>
		/// <typeparam name="T">The type, to which the response json will be parsed</typeparam>
		/// <param name="uri">Trakt Api endpoint URI to be called</param>
		/// <returns>Api json response persed to T template type</returns>
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
					json = await response.Content.ReadAsStringAsync();

					// Convert json result to return type
					T result = JsonConvert.DeserializeObject<T>(json);

					return result;
				}
				catch (JsonSerializationException ex)
				{
					System.Diagnostics.Debug.WriteLine($"Trakt API returned unexpected response json.");
					System.Diagnostics.Debug.WriteLine($"Exception: {ex.StackTrace}\nMessage: {ex.Message}");
				}
				catch (Exception ex)
				{
					// Handling general exceptions
					System.Diagnostics.Debug.WriteLine($"Some other exception occured.");
					System.Diagnostics.Debug.WriteLine($"Exception: {ex.StackTrace}\nMessage: {ex.Message}");
				}

				return default(T);
			}
		}

	}
}
