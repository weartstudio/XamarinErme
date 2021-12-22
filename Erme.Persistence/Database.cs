using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Erme.Persistence
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Spent>();
            _database.CreateTableAsync<Category>();
            _database.CreateTableAsync<Budget>();
        }

        // read
        public async Task<List<Spent>> GetSpentAsync()
        {
            return await Task.Run(() => _database.Table<Spent>().ToListAsync());
        }
        public async Task<List<Budget>> GetBudgetAsync()
        {
            return await Task.Run(() => _database.Table<Budget>().ToListAsync());
        }
        public async Task<List<Category>> GetCatAsync()
        {
            return await Task.Run(() => _database.Table<Category>().ToListAsync());
        }
        public async Task<Category> GetSpecCatAsync(string t)
        {
            return await Task.Run(() => _database.Table<Category>().Where(cat => cat.Title == t).FirstOrDefaultAsync());
        }
        public async Task<Category> GetCatByIdAsync(int t)
        {
            return await Task.Run(() => _database.Table<Category>().Where(cat => cat.Id == t).FirstOrDefaultAsync());
        }

        // insert
        public Task<int> Add(System.Object o)
        {
            return _database.InsertAsync(o);
        }

        // delete
        public Task<int> Delete(System.Object o)
        {
            return _database.DeleteAsync(o);
        }

    }
}
