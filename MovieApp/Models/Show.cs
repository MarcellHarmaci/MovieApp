using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
	public class Show
	{
		public string Title { get; set; }
		public int? Year { get; set; }
		public Ids Ids { get; set; }
	}
}
