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
    public class ClockRepository
    {
        SQLiteConnection database;

        public ClockRepository(string filename)
        {            
            database = new SQLiteConnection(DependencyService.Get<ISQLite>().GetDatabasePath(filename));
            database.CreateTable<DBUser>();
            database.CreateTable<DBClock>();
            new MockData(database);
        }

        public List<DBUser> GetUsers()
        {
            return (from i in database.Table<DBUser>() select i).ToList();
        }

        public int SaveItem(DBUser item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        public bool LogIn(string login, string password)
        {
            var userLog = from p in database.Table<DBUser>()
                       where p.Login == login
                       where p.Password == password
                       select p;

            if(userLog.SingleOrDefault() == null) 
                return false;
            else
                return true;
        }
             
        public List<ClockModel> GetClocks(int id)
        {
            var dbuser = database.GetWithChildren<DBUser>(id);

            List<ClockModel> clockModelList = new List<ClockModel>();
            foreach(var item in dbuser.Clocks)
            {
                ClockModel cm = new ClockModel()
                {
                    ClockTimeZone = TimeZoneInfo.FindSystemTimeZoneById(item.ClockTimeZoneId),
                    HeadColor = Color.FromHex(item.ClockHeadColor),
                    FaceColor = Color.FromHex(item.ClockFaceColor)
                };
                clockModelList.Add(cm);
            }
            return clockModelList;
        }

        public int GetUserId(string login)
        {
            var userLog = from p in database.Table<DBUser>()
                          where p.Login == login
                          select p;
             return userLog.SingleOrDefault().Id;
        }

        public DBUser GetUser(int id)
        {
            return database.GetWithChildren<DBUser>(id);         
        }

        public async void UpdateClockListAsync(List<ClockModel> clockList, int id)
        {
            ClearClockList(id);

            await Task.Run(() =>
            {
                foreach (var item in clockList)
                {
                    AddClock(item, id);
                }
            });
        }  

        private void ClearClockList(int id)
        {
            DBUser user = GetUser(id);
            user.Clocks.Clear();
            database.UpdateWithChildren(user);
        }
        
        public void AddClock(ClockModel clockModel, int id)
        {
           
                DBUser user = GetUser(id);

                DBClock dBClock = new DBClock()
                {
                    ClockHeadColor = ColorConverter.ColorToHex(clockModel.HeadColor),
                    ClockFaceColor = ColorConverter.ColorToHex(clockModel.FaceColor),
                    ClockTimeZoneId = clockModel.ClockTimeZone.Id
                };
                database.Insert(dBClock);
                user.Clocks.Add(dBClock);
                database.UpdateWithChildren(user);
         
        }      
    }
}
