using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HOFCCross.Database;
using System.IO;
using Xamarin.Forms;
using HOFCCross.Droid.Database;
using SQLite;

[assembly: Dependency(typeof(SQLiteAndroid))]
namespace HOFCCross.Droid.Database
{
    public class SQLiteAndroid : HOFCCross.Database.Database
    {
        protected override SQLiteAsyncConnection CreateConnection()
        {
            var sqliteFilename = "HOFC.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            return new SQLiteAsyncConnection(path);
        }
    }
}