using ExampleGenericRepository.Interfaces;
using ExampleGenericRepository.iOS;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace ExampleGenericRepository.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var fileName = "ExempDB.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}
