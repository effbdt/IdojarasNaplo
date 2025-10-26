using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public partial class Diary : ObservableObject
	{
		[ObservableProperty]
		string title;

		[ObservableProperty]
		string body;

		[ObservableProperty]
		string photopath;

		[ObservableProperty]
		string location;

		[ObservableProperty]
		string weather;

		public Diary GetCopy()
		{
			return (Diary)this.MemberwiseClone();
		}
	}
}
