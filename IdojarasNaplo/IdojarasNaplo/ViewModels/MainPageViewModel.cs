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
	[QueryProperty(nameof(EditedDiary), "DiaryEntry")]
	public partial class MainPageViewModel : ObservableObject
	{
		public ObservableCollection<Diary> Diaries { get; set; }

		private string dataBasePath = Path.Combine(FileSystem.Current.AppDataDirectory, "diaries.db3");
		private SQLiteAsyncConnection db;

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
			db = new SQLiteAsyncConnection(dataBasePath);
			db.CreateTableAsync<Diary>().Wait();
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
		public async Task SaveAsync()
		{
			if (SelectedDiary == null)
			{
				EditedDiary.Id = Diaries.Count == 0 ? 1 : (Diaries.Max(d => d.Id) + 1);
				Diaries.Add(EditedDiary);

				await db.InsertAsync(EditedDiary);
				EditedDiary = null;
			}
			else
			{
				int index = Diaries.IndexOf(SelectedDiary);
				Diaries[index] = EditedDiary;

				await db.UpdateAsync(EditedDiary);

				SelectedDiary = null;
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
