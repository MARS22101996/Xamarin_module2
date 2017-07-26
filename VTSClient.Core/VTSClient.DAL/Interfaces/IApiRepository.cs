using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTSClient.DAL.Entities;

namespace VTSClient.DAL.Interfaces
{
    public interface IApiRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
