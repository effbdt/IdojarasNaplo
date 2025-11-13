using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	[QueryProperty(nameof(EditedDiary), "EditedDiary")]
	public partial class MainPageViewModel : ObservableObject
	{
		public ObservableCollection<Diary> Diaries { get; set; }

		private readonly IDiaryDatabase _db;

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
					SaveDiaryToCollectionAndDb(value);
				}
			}
		}

		public bool IsFullViewEnabled
		{

			get
			{
				return Preferences.Default.Get("fullview", true);
			}
			set
			{
				Preferences.Default.Set("fullview", value);
				OnPropertyChanged();
			}
		}


		public MainPageViewModel(IDiaryDatabase db)
		{
			_db = db;
			Diaries = new ObservableCollection<Diary>();
			LoadDiaries();
		}

		public async void LoadDiaries()
		{
			Diaries.Clear();
			var list = await _db.GetEntries();
			foreach (var d in list)
			{
				Diaries.Add(d);
			}
		}

		private async void SaveDiaryToCollectionAndDb(Diary diary)
		{
			var existing = Diaries.FirstOrDefault(d => d.Id == diary.Id);

			if (existing == null || diary.Id == 0)
			{
				await _db.CreateEntryAsync(diary);
				Diaries.Add(diary);
			}
			else
			{
				await _db.UpdateEntryAsync(diary);

				var index = Diaries.IndexOf(existing);
				Diaries[index] = diary;
			}

		}


		[RelayCommand]
		public async Task NewDiaryEntryAsync()
		{
			SelectedDiary = null;
			var newDiary = new Diary
			{
				Date = DateTime.Today,
			};

			var param = new ShellNavigationQueryParameters
			{
				{"DiaryEntry", newDiary }
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
		public async Task DeleteEntry()
		{
			if (SelectedDiary != null)
			{
				await _db.DeleteEntryAsync(SelectedDiary);
				Diaries.Remove(SelectedDiary);
				SelectedDiary = null;
			}
			else
			{
				WeakReferenceMessenger.Default.Send("Select a diary entry to delete.");
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
				await Shell.Current.GoToAsync("diaryDetails", param);
			}
			else
			{
				WeakReferenceMessenger.Default.Send("Select a diary entry to see it's details.");
			}
		}

		[RelayCommand]
		public async Task GoToServicesPage()
		{
			await Shell.Current.GoToAsync("servicePage");
		}
	}
}
