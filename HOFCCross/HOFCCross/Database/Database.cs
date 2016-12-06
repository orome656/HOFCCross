using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using SQLite;

namespace HOFCCross.Database
{
    public abstract class Database : ISQLite
    {
        protected abstract SQLiteAsyncConnection CreateConnection();
        public async Task<SQLiteAsyncConnection> GetConnection()
        {
            var connection = CreateConnection();

            await connection.CreateTableAsync<Actu>();
            await connection.CreateTableAsync<Competition>();
            await connection.CreateTableAsync<ClassementEquipe>();
            await connection.CreateTableAsync<Match>();
            await connection.CreateTableAsync<ArticleDetails>();
            await connection.CreateTableAsync<Diaporama>();
            await connection.CreateTableAsync<MatchInfos>();

            return connection;
        }
        
    }
}
