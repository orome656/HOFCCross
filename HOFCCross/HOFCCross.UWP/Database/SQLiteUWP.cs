using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;
using System.IO;
using Xamarin.Forms;
using HOFCCross.UWP.Database;

[assembly: Dependency(typeof(SQLiteUWP))]
namespace HOFCCross.UWP.Database
{
    public class SQLiteUWP : HOFCCross.Database.Database
    {
        protected override SQLiteAsyncConnection CreateConnection()
        {
            var databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "HOFC.db3");
            return new SQLiteAsyncConnection(databasePath);
        }
    }
}
