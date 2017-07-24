using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTSClient.BLL.Dto;

namespace VTSClient.BLL.Interfaces
{
    public interface IApiVacationService
    {
        Task<IEnumerable<VacationDto>> GetVacationAsync();
        Task<VacationDto> GetVacationByIdAsync(Guid id);
        Task<bool> CreateVacationAsync(VacationDto entity);
        Task<bool> UpdateVacationAsync(VacationDto entity);
        Task<bool> DeleteVacationByIdAsync(Guid id);
    }
}
