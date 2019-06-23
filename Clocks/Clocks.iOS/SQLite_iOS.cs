using System;
using Xamarin.Forms;
using System.IO;
using Clocks.Services;
using Clocks.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace Clocks.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);

            return path;
        }
    }
}