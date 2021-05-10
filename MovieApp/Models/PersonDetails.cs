using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class PersonDetails
	{
		public string Name { get; set; }
		public Ids Ids { get; set; }

		[JsonProperty("social_ids")]
		public SocialIds SocialIds { get; set; }
		public string Biography { get; set; }
		public string Birthday { get; set; }
		public string Death { get; set; }
		public string Birthplace { get; set; }
		public string Homepage { get; set; }
	}

}
