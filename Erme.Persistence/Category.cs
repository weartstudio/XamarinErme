using System;
using SQLite;

namespace Erme.Persistence
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
