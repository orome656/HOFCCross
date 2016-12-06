using HOFCCross.Database;
using SQLite;
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
        protected SQLiteAsyncConnection _connection;

        public Repository()
        {
            _connection = Task.Run(() => DependencyService.Get<ISQLite>().GetConnection()).Result;
            _connection.CreateTableAsync<T>();
        }

        public AsyncTableQuery<T> AsQueryable() => _connection.Table<T>();

        public async Task<List<T>> Get() => await _connection.Table<T>().ToListAsync();

        public async Task<T> Get(object key) => await _connection.FindAsync<T>(key);

        public virtual Task<List<T>> GetWithChildren()
        {
            throw new NotImplementedException();
        }
        
        public virtual async Task InsertOrUpdate(T entity) => await _connection.InsertOrReplaceAsync(entity);
        
        public virtual async Task InsertOrUpdateList(List<T> entities)
        {
            List<Task> tasks = new List<Task>();

            foreach (var entite in entities)
            {
                tasks.Add(_connection.InsertOrReplaceAsync(entite));
            }

            await Task.WhenAll(tasks.ToArray());
        }

        public virtual void DeleteAll()
        {
            _connection.GetConnection().DeleteAll<T>();
        }
    }
}
