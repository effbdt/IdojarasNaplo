using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public interface IDiaryDatabase
	{
		Task CreateEntryAsync(Diary diary);

		Task<Diary> GetEntryAsync(int id);

		Task<List<Diary>> GetEntries();

		Task DeleteEntryAsync(Diary diary);

		Task UpdateEntryAsync(Diary diary);

	}
}
