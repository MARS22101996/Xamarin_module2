using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Infrastructure;
using VTSClient.DAL.Interfaces;

namespace VTSClient.DAL.Repositories
{
    public class SqlRepositoryVacation : ISqlRepositoryVacation, IDisposable
    {
        private  SQLiteConnection _context;

        public SqlRepositoryVacation(IDbLocation dbLocation)
        {
            _context = new SQLiteConnection(dbLocation.GetDatabasePath(DbNameSettings.DbName));

            _context.CreateTable<Vacation>();
        }

        public IEnumerable<Vacation> GetAll()
        {
            return _context.Table<Vacation>();
        }

        public Vacation GetById(Guid id)
        {
            return _context.Get<Vacation>(id);
        }

        public void Create(Vacation item)
        {
            _context.Insert(item);
        }

        public void Delete(Guid id)
        {
            var item = _context.Get<Vacation>(id);
            if (item == null) return;
            _context.Delete(item);
        }

        public void Update(Vacation item)
        {
            _context.Update(item);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
