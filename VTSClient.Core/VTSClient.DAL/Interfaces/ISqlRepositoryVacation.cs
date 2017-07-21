using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTSClient.DAL.Entities;

namespace VTSClient.DAL.Interfaces
{
    public interface ISqlRepositoryVacation
    {
        void Create(Vacation item);
        void Update(Vacation item);
        void Delete(Guid id);
        IEnumerable<Vacation> GetAll();
        Vacation GetById(Guid id);
    }
}
