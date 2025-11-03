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

		[RelayCommand]
		public async Task OpenImageAsync()
		{
			var image = await MediaPicker.Default.PickPhotoAsync();

			if (image != null)
			{
				string localURL = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
				if (!File.Exists(localURL))
				{
					using Stream stream = await image.OpenReadAsync();
					using FileStream fs = File.OpenWrite(localURL);
					await stream.CopyToAsync(fs);
				}
				EditedDiary.Photopath = localURL;
			}
		}

		[RelayCommand]
		public async Task TakePhotoAsync()
		{
			if (!MediaPicker.Default.IsCaptureSupported) return;

			var image = await MediaPicker.Default.CapturePhotoAsync();
			if (image != null)
			{
				string localURL = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
				if (!File.Exists(localURL))
				{
					using Stream stream = await image.OpenReadAsync();
					using FileStream fs = File.OpenWrite(localURL);
					await stream.CopyToAsync(fs);
				}
				EditedDiary.Photopath = localURL;
			}
		}
	}
}
