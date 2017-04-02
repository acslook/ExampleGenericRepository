using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using ExampleGenericRepository.Droid;
using ExampleGenericRepository.Interfaces;

[assembly: Dependency(typeof(SQLite_Android))]
namespace ExampleGenericRepository.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "ExempDB.db3";
            var documentspath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);
            return connection;
        }
    }
}