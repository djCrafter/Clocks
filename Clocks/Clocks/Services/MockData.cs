using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using Clocks.Models;
using System.Threading.Tasks;

namespace Clocks.Services
{
    public class MockData
    {
        public MockData(SQLiteConnection database)
        {
            var users = from p in database.Table<DBUser>() select p;
            if (users.Count() == 0)
            {
                List<DBClock> dBClocksList1 = new List<DBClock>()
            {
             new DBClock()
            {
                ClockHeadColor = ColorConverter.ColorToHex(Color.Red),
                ClockFaceColor = ColorConverter.ColorToHex(Color.Black),
                ClockTimeZoneId = TimeZoneInfo.Local.Id
            },
             new DBClock()
            {
                ClockHeadColor = ColorConverter.ColorToHex(Color.Green),
                ClockFaceColor = ColorConverter.ColorToHex(Color.Yellow),
                ClockTimeZoneId = TimeZoneInfo.Local.Id
            }
           };

                List<DBClock> dBClocksList2 = new List<DBClock>()
            {
             new DBClock()
            {
                ClockHeadColor = ColorConverter.ColorToHex(Color.Purple),
                ClockFaceColor = ColorConverter.ColorToHex(Color.Blue),
                ClockTimeZoneId = TimeZoneInfo.Local.Id
             }
            };

                DBUser user1 = new DBUser()
                {
                    Login = "Admin",
                    Password = "12345",
                };
                DBUser user2 = new DBUser()
                {
                    Login = "Vaso",
                    Password = "Rubenovich"
                };

                database.Insert(user1);
                database.InsertAll(dBClocksList1);
                user1.Clocks = dBClocksList1;
                database.UpdateWithChildren(user1);

                database.Insert(user2);
                database.InsertAll(dBClocksList2);
                user2.Clocks = dBClocksList2;
                database.UpdateWithChildren(user2);
            }
        }
    }
}
