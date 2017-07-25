using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VTSClient.BLL.Dto;
using VTSClient.BLL.Interfaces;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Interfaces;

namespace VTSClient.BLL.Services
{
    public class ApiVacationService : IApiVacationService
    {
        private readonly IApiRepositoryVacation _vacationRepository;

        public ApiVacationService(IApiRepositoryVacation vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public async Task<IEnumerable<VacationDto>> GetVacationAsync()
        {
            var vacations = await _vacationRepository.GetAsync();

            var vacationDtos = Mapper.Map<IEnumerable<VacationDto>>(vacations);

            return vacationDtos.ToList();
        }

        public async Task<VacationDto> GetVacationByIdAsync(Guid id)
        {
            var vacation = await _vacationRepository.GetByIdAsync(id);

            var vacationDto = Mapper.Map<VacationDto>(vacation);

            return vacationDto;
        }

        public Task<bool> CreateVacationAsync(VacationDto entity)
        {
            var vacation = Mapper.Map<Vacation>(entity);

            return _vacationRepository.CreateAsync(vacation);
        }

        public Task<bool> UpdateVacationAsync(VacationDto entity)
        {
            var vacation = Mapper.Map<Vacation>(entity);

            return _vacationRepository.UpdateAsync(vacation);
        }

        public Task<bool> DeleteVacationByIdAsync(Guid id)
        {
            return _vacationRepository.DeleteAsync(id);
        }
    }
}
