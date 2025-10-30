using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public class SQLiteDiaryEntryDatabase : IDiaryDatabase
	{
		private string databasePath = Path.Combine(FileSystem.Current.AppDataDirectory, "diaries.db3");

		SQLite.SQLiteOpenFlags Flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite;

		SQLiteAsyncConnection database;

		public SQLiteDiaryEntryDatabase()
		{
			database = new SQLiteAsyncConnection(databasePath, Flags);
			database.CreateTableAsync<Diary>().Wait();
		}

		public async Task CreateEntryAsync(Diary diary)
		{
			await database.InsertAsync(diary);
		}

		public async Task DeleteEntryAsync(Diary diary)
		{
			await database.DeleteAsync(diary);
		}

		public async Task<List<Diary>> GetEntries()
		{
			return await database.Table<Diary>().ToListAsync();
		}

		public async Task<Diary> GetEntryAsync(int id)
		{
			return await database.GetAsync<Diary>(id);
		}

		public async Task UpdateEntryAsync(Diary diary)
		{
			await database.UpdateAsync(diary);
		}
	}
}
