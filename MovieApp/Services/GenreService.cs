using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
	class GenreService : TraktService
	{
		public async Task<List<Genre>> GetMovieGenresAsync()
		{
			string relativeUri = $"genres/movies";
			Uri uri = new Uri(serverUrl, relativeUri);
			return await GetAsync<List<Genre>>(uri);
		}
	}
}
