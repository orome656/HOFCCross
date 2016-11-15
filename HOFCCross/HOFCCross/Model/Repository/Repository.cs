using HOFCCross.Database;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Model.Repository
{
    public class Repository<T> where T : class, new() // Créer une interface pour les entitées
    {
        SQLiteConnection _connection;

        public Repository()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<T>();
        }

        public TableQuery<T> AsQueryable() => _connection.Table<T>();

        public List<T> Get() => _connection.Table<T>().ToList();

        public int Insert(T entity) => _connection.Insert(entity);
    }
}
