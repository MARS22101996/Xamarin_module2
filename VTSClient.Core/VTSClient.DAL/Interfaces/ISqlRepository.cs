using System;
using System.Collections.Generic;

namespace VTSClient.DAL.Interfaces
{
    public interface ISqlRepository<T> where T : class
    {
        void Create(T item);

        void Update(T item);

        void Delete(Guid id);

        IEnumerable<T> GetAll();

        T GetById(Guid id);
    }
}
