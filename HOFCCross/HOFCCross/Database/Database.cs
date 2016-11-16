using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using HOFCCross.Model;

namespace HOFCCross.Database
{
    public abstract class Database : ISQLite
    {
        protected abstract SQLiteConnection CreateConnection();
        public SQLiteConnection GetConnection()
        {
            var connection = CreateConnection();

            connection.CreateTable<Actu>();
            connection.CreateTable<Competition>();
            connection.CreateTable<Match>();

            return connection;
        }
        
    }
}
