using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Infrastructure;
using VTSClient.DAL.Interfaces;

namespace VTSClient.DAL.Repositories
{
    public class CommonSqlRepository<TEntity> : ISqlRepository<TEntity> where TEntity : class, new()
    {
        private SQLiteConnection _context;

        public CommonSqlRepository(IDbLocation dbLocation)
        {
            _context = new SQLiteConnection(dbLocation.GetDatabasePath(DbNameSettings.DbName));

            _context.CreateTable<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Table<TEntity>();
        }

        public TEntity GetById(Guid id)
        {
            return _context.Get<TEntity>(id);
        }

        public void Create(TEntity item)
        {
            _context.Insert(item);
        }

        public void Delete(Guid id)
        {
            var item = _context.Get<Vacation>(id);
            if (item == null) return;
            _context.Delete(item);
        }

        public void Update(TEntity item)
        {
            _context.Update(item);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}
