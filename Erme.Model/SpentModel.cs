using System;
using Erme.Persistence;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Erme.Model
{
    public class SpentModel : ErmeModel
    {

        public async Task<List<Spent>> GetListSpent()
        {
            return await Database.GetSpentAsync();
        }

        public async Task<List<Spent>> GetListMonthSpent()
        {
            List<Spent> spents = await Database.GetSpentAsync();
            List<Spent> months = new List<Spent>();

            return spents;
        }

        public async Task<int> AddSpent(String t, int a, DateTime d, string c)
        {
            Spent spent = new Spent();
            spent.Title = t;
            spent.Amount = a;
            spent.Date = d;
            var cat = await Database.GetSpecCatAsync(c);
            spent.CategoryId = cat.Id;
            return await Database.Add(spent);
        }

        public async Task<List<Spent>> CollectedSpents(string x)
        {
            List<Spent> spents = await GetListSpent();
            List<Spent> spents2 = new List<Spent>();
            foreach (var item in spents)
            {
                string datumS = item.Date.Year.ToString() + "-" + item.Date.Month.ToString();
                if (x == datumS)
                {
                    spents2.Add(item);
                }
            }
            return spents2;
        }

        public async Task<bool> Alert_if_too_much(string x)
        {
            int spentAll = 0;
            int budgetAll = 0;
            List<Spent> spents = await GetListSpent();
            List<Budget> budgets = await GetListBudget();

            foreach (var item in spents)
            {
                string datumS = item.Date.Year.ToString() + "-" + item.Date.Month.ToString();
                if (x == datumS)
                {
                    spentAll += item.Amount;
                }
            }

            foreach (var item in budgets)
            {
                string datumS = item.Month.Year.ToString() + "-" + item.Month.Month.ToString();

                if (x == datumS)
                {
                    budgetAll = (int)(item.Amount * (item.Alert * 0.01));
                    break; // csak az első kell
                }
            }

            return (spentAll > budgetAll && budgetAll != 0 && spentAll != 0) ? true : false;


        }

        public async Task<List<string>> GetSpentDates()
        { 
            List<Spent> spents = await this.GetListSpent();
            
            List<DateTime> x = spents.Select(d => new DateTime(d.Date.Year, d.Date.Month, 1)).Distinct().ToList();
            List<string> datumok = new List<string>();
            x.Sort();
            foreach (var item in x)
            {
                string datumS = item.Year.ToString() + "-" + item.Month.ToString();
                //HonapValaszto.Items.Add(datumS);
                datumok.Add(datumS);
            }

            return datumok;

        }



    }
}
