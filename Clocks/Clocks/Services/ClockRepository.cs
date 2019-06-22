using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using Clocks.Models;

namespace Clocks.Services
{
    public class ClockRepository
    {
        SQLiteConnection database;

        public ClockRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<DBUser>();
            database.CreateTable<DBClock>();
        }

        public List<DBUser> GetUsers()
        {
            return (from i in database.Table<DBUser>() select i).ToList();
        }
    }
}
