using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	[QueryProperty(nameof(Diaries), "AllDiaries")]
	public partial class ServicesViewModel : ObservableObject
	{
		[ObservableProperty]
		Diary[] diaries;




	}
}
