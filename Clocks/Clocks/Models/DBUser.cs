using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clocks.Models
{
    public class DBUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Login { get; set; }
        public string Password { get; set; }

        [OneToMany]
        public List<DBClock> Clocks { get; set; }
    }
}
