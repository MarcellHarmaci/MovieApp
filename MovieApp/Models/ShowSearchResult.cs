using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class ShowSearchResult
	{
		public string Type { get; set; }
		public float Score { get; set; }
		public Show Show { get; set; }
	}

}
