using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public class WeatherResponse
	{
		public WeatherMain Main { get; set; }
		public List<WeatherItem> Weather { get; set; }
	}

	public class WeatherMain
	{
		public double Temp { get; set; }
	}

	public class WeatherItem
	{
		public string Description { get; set; }
	}


}
