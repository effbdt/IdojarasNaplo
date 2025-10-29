using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	[QueryProperty(nameof(EditedDiary), "DiaryEntry")]
	public partial class EditDiaryEntryViewModel : ObservableObject
	{
		[ObservableProperty]
		Diary editedDiary;

		[ObservableProperty]
		Diary draft;

		public void InitDraft()
		{
			Draft = EditedDiary.GetCopy();
		}

		[RelayCommand]
		public async Task SaveDiaryEntry()
		{
			var param = new ShellNavigationQueryParameters
			{
				{"EditedDiary", Draft }
			};
			await Shell.Current.GoToAsync("..", param);
		}

		[RelayCommand]
		public async Task CancelEdit()
		{
			await Shell.Current.GoToAsync("..");
		}
	}


}
