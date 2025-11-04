using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoogleGson;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
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

		public string WeatherDescription { get; set; }
		public double Temperature { get; set; }


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
				Draft.Photopath = localURL;
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
				Draft.Photopath = localURL;
				EditedDiary.Photopath = localURL;
			}
		}

		private async Task GetLocationAsync()
		{
			var location = await Geolocation.GetLastKnownLocationAsync();
			double latitude = location?.Latitude ?? 0;
			double longitude = location?.Longitude ?? 0;


			EditedDiary.Latitude = latitude;
			EditedDiary.Longitude = longitude;

			Draft.Latitude = latitude;
			Draft.Longitude = longitude;
		}

		private static readonly JsonSerializerOptions jsonOptions =
	new() { PropertyNameCaseInsensitive = true };

		[RelayCommand]
		public async Task GetWeatherAsync()
		{
			await GetLocationAsync();
			if (EditedDiary.Latitude == null || EditedDiary.Longitude == null)
				return;

			string apiKey = "4643e617cca60585379ad4d2f6585636";

			string url =
				$"https://api.openweathermap.org/data/2.5/weather?lat={EditedDiary.Latitude}&lon={EditedDiary.Longitude}&units=metric&appid={apiKey}";

			using var client = new HttpClient();
			var json = await client.GetStringAsync(url);
			var data = JsonSerializer.Deserialize<WeatherResponse>(json, jsonOptions);

			WeatherDescription = data?.Weather?[0].Description ?? "Unknown";
			Temperature = data?.Main?.Temp ?? 0;

			OnPropertyChanged(nameof(WeatherDescription));
			OnPropertyChanged(nameof(Temperature));
		}

	}
}
