using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public partial class MainPageViewModel : ObservableObject
	{
		public ObservableCollection<Diary> Diaries { get; set; }

		[ObservableProperty]
		Diary selectedDiary;

		public Diary EditedDiary
		{
			set
			{
				if (value == null)
				{
					if (SelectedDiary != null)
					{
						Diaries.Remove(SelectedDiary);
						SelectedDiary = null;
					}
					Diaries.Add(value);
				}
			}
		}

		public MainPageViewModel()
		{
			Diaries = new ObservableCollection<Diary>();
		}

		[RelayCommand]
		public async Task NewDiaryEntryAsync()
		{
			SelectedDiary = null;
			var param = new ShellNavigationQueryParameters
			{
				{"DiaryEntry", new Diary() }
			};
			await Shell.Current.GoToAsync("editDiary", param);
		}

		[RelayCommand]
		public async Task EditDiaryEntryAsync()
		{
			if (SelectedDiary != null)
			{
				var param = new ShellNavigationQueryParameters
				{
					{"DiaryEntry", SelectedDiary}
				};
				await Shell.Current.GoToAsync("editDiary", param);
			}
			else
			{
				WeakReferenceMessenger.Default.Send("Select a diary entry to edit.");
			}
		}

		[RelayCommand]
		public void DeleteEntry()
		{
			if (SelectedDiary != null)
			{
				Diaries.Remove(SelectedDiary);
				SelectedDiary = null;
			}
			else
			{
				WeakReferenceMessenger.Default.Send("Select a diary entry to delete");
			}

		}

		[RelayCommand]
		public async Task ShowDiaryDetailsAsync()
		{
			if (SelectedDiary != null)
			{
				var param = new ShellNavigationQueryParameters
				{
					{"DiaryEntry", SelectedDiary}
				};
				await Shell.Current.GoToAsync("entryDetails", param);
			}

		}
	}
}
