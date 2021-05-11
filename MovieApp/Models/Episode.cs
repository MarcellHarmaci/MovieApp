using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class Episode
	{
		public int Season { get; set; }
		public int Number { get; set; }
		public string Title { get; set; }
		public Ids Ids { get; set; }
	}

}
