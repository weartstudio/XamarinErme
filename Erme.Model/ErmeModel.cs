using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Erme.Persistence;

namespace Erme.Model
{
    public class ErmeModel
    {
        // adatbázis kapcsolódás és létrehozás
        private static Database database; // csak tároláshoz
        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database( Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ), "erme.db3" ));
                }

                return database;
            }
        }

        // category
        public async Task<List<Category>> GetListCat()
        {
            return await Database.GetCatAsync();
        }
        public async Task<Category> GetSpecificCategoryById(int i)
        {
            return await Database.GetCatByIdAsync(i);
        }
        public async Task<int> AddCategory(String s)
        {
            Category cat = new Category();
            cat.Title = s;
            return await Database.Add(cat);
        }

        //budget
        public async Task<List<Budget>> GetListBudget()
        {
            return await Database.GetBudgetAsync();
        }
        public async Task<int> AddBudget(int a, int sz, DateTime d)
        {
            Budget budget = new Budget();
            budget.Amount = a;
            budget.Alert = sz;
            budget.Month = d;
            return await Database.Add(budget);
        }

        // delete anywhere
        public async Task<int> Delete(System.Object c)
        {
            return await Database.Delete(c);
        }

        


    }
}
