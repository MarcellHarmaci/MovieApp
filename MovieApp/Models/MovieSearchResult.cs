using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class MovieSearchResult
	{
		public string Type { get; set; }
		public double Score { get; set; }
		public Movie Movie { get; set; }
	}

}
