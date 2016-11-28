using HOFCCross.Database;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
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
        protected SQLiteConnection _connection;

        public Repository()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<T>();
        }

        public TableQuery<T> AsQueryable() => _connection.Table<T>();

        public List<T> Get() => _connection.Table<T>().ToList();

        public T Get(object key) => _connection.FindWithChildren<T>(key);

        public virtual List<T> GetWithChildren() => _connection.GetAllWithChildren<T>(recursive: true);

        public virtual void Insert(T entity) => _connection.InsertWithChildren(entity, recursive: true);
        
        public virtual void InsertOrUpdate(T entity)
        {
            _connection.InsertOrReplaceWithChildren(entity);
        }

        public virtual void InsertOrUpdateList(List<T> entities)
        {
            _connection.InsertOrReplaceAllWithChildren(entities);
        }

        public virtual void DeleteAll()
        {
            _connection.DeleteAll<T>();
        }
    }
}
