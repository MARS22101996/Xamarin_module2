using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTSClient.DAL.Entities;

namespace VTSClient.DAL.Interfaces
{
    public interface IApiRepositoryVacation
    {
        Task<IEnumerable<Vacation>> GetAsync();

        Task<Vacation> GetByIdAsync(Guid id);

        Task<bool> CreateAsync(Vacation entity);

        Task<bool> UpdateAsync(Vacation entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
