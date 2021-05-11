using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class SeasonDetails
	{
		public int Number { get; set; }
		public Ids Ids { get; set; }
		public double Rating { get; set; }
		public int Votes { get; set; }

		[JsonProperty("episode_count")]
		public int EpisodeCount { get; set; }

		[JsonProperty("aired_episodes")]
		public int AiredEpisodes { get; set; }
		public string Title { get; set; }
		public string Overview { get; set; }

		[JsonProperty("first_aired")]
		public DateTime FirstAired { get; set; }

		[JsonProperty("updated_at")]
		public string UdpatedAt { get; set; }
		public string Network { get; set; }

		public List<Episode> Episodes { get; set; } = new List<Episode>();
	}

}
