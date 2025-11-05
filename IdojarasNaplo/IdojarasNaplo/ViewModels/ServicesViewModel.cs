using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IdojarasNaplo
{
	public partial class ServicesViewModel : ObservableObject
	{
		[ObservableProperty]
		ObservableCollection<Diary> diaries;

		readonly IDiaryDatabase _db;

		public ServicesViewModel(IDiaryDatabase db)
		{
			_db = db;
			LoadDiaries();
		}
		private async Task LoadDiaries()
		{
			var all = await _db.GetEntries();
			Diaries = new ObservableCollection<Diary>(all);
		}

		[ObservableProperty]
		double? avgTemperature;

		[ObservableProperty]
		double? coldestTemperature;

		[ObservableProperty]
		double? hottestTemperature;

		[RelayCommand]
		public void GetAvgTemperature()
		{

			var values = diaries.
				Where(d => d.Temperature.HasValue)
				.Select(d => d.Temperature.Value);

			AvgTemperature = values.Any() ? values.Average() : null;
		}

		[RelayCommand]
		public void GetColdestTemperature()
		{
			var values = diaries.
				Where(d => d.Temperature.HasValue).
				Select(d => d.Temperature.Value);

			ColdestTemperature = values.Any() ? values.Min() : null;
		}

		[RelayCommand]
		public void GetHottestTemperature()
		{
			var values = diaries.
				Where(d => d.Temperature.HasValue).
				Select(d => d.Temperature.Value);

			HottestTemperature = values.Any() ? values.Max() : null;
		}

	}
}
