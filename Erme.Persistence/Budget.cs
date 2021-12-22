using System;
using SQLite;
namespace Erme.Persistence
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Alert { get; set; }
        public DateTime Month { get; set; }
    }
}
