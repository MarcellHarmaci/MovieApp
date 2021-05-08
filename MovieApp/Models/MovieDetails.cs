using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class MovieDetails
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public Ids Ids { get; set; }
		public string Tagline { get; set; }
		public string Overview { get; set; }
		public string Released { get; set; }
		public int Runtime { get; set; }
		public string Country { get; set; }

		[JsonProperty("updated_at")]
		public DateTime UpdatedAt { get; set; }
		public object Trailer { get; set; }
		public string Homepage { get; set; }
		public string Status { get; set; }
		public double Rating { get; set; }
		public int Votes { get; set; }

		[JsonProperty("comment_count")]
		public int CommentCount { get; set; }
		public string Language { get; set; }

		[JsonProperty("available_translations")]
		public string[] AvailableTranslations { get; set; }
		public string[] Genres { get; set; }
		public string Certification { get; set; }
	}

}
