using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	[QueryProperty(nameof(EditedDiary), "EditedDiary")]
	public partial class MainPageViewModel : ObservableObject
	{
		public ObservableCollection<Diary> Diaries { get; set; }

		[ObservableProperty]
		Diary selectedDiary;

		private Diary _editedDiary;

		public Diary EditedDiary
		{
			get { return _editedDiary; }
			set
			{
				if (SetProperty(ref _editedDiary, value) && value != null)
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
			Diaries.Add(new Diary() { Title = "Today", Body = "Test", Location = "123", Weather = "good", Photopath = "test" });
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

		public async Task ShowDiaryDetails()
		{
			if (SelectedDiary != null)
			{
				var param = new ShellNavigationQueryParameters
				{
					{"Diary", SelectedDiary }
				};
				await Shell.Current.GoToAsync("diarydetails", param);
			}
		}


		[RelayCommand]
		public async Task DeleteEntry()
		{
			if (SelectedDiary != null)
			{
				await db.DeleteAsync(SelectedDiary);
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
