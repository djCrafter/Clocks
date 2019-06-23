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
        
        public string ClockHeadColor { get; set; }

        public string ClockFaceColor { get; set; }

        public string ClockTimeZoneId { get; set; }
    }
}
