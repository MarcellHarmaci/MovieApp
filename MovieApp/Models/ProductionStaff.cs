using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{

	public class ProductionStaff
	{
		public Cast[] Cast { get; set; }
		public Crew Crew { get; set; }
	}

	public class Crew
	{
		public Production[] Production { get; set; }
		public Art[] Art { get; set; }
		public Crew[] Crew2 { get; set; }
		public CostumeMakeUp[] Dostumemakeup { get; set; }
		public Directing[] Directing { get; set; }
		public Writing[] Writing { get; set; }
		public Sound[] Sound { get; set; }
		public Camera[] Camera { get; set; }
	}

	public class Production
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
		public string Job { get; set; }
	}

	public class Art
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
	}

	public class CostumeMakeUp
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
	}

	public class Directing
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
	}

	public class Writing
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
		public string Job { get; set; }
	}

	public class Sound
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
	}


	public class Camera
	{
		public string[] Jobs { get; set; }
		public Person Person { get; set; }
	}

	public class Cast
	{
		public string Character { get; set; }
		public string[] Characters { get; set; }
		public Person Person { get; set; }
	}

}
