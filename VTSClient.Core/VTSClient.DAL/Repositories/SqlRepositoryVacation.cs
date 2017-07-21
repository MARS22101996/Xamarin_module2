using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Async;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Interfaces;

namespace VTSClient.DAL.Repositories
{
    public class SqlRepositoryVacation
    {
        private readonly SQLiteConnection _context;

        public SqlRepositoryVacation(IDbLocation dbLocation)
        {
            _context = new SQLiteConnection(dbLocation.GetDatabasePath(DalSettings.DbName));
        }

        public  IEnumerable<Vacation> GetAsync()
        {
            return _context.Table<Vacation>().ToList();
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
            _context.Insert(item);
        }
    }
}
