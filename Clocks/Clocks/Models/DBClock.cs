using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clocks.Models
{
    public class DBClock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(DBUser))]
        public int DBUserId { get; set; }      
        public double Head_R { get; set; }
        public double Head_G { get; set; }
        public double Head_B { get; set; }
        public double Face_R { get; set; }
        public double Face_G { get; set; }
        public double Face_B { get; set; }       
        public string ClockTimeZoneId { get; set; }
    }
}
