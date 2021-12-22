using System;
using SQLite;

namespace Erme.Persistence
{
    public class Spent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
    }
}
