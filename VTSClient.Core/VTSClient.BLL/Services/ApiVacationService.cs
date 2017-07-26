using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IApiRepository<Vacation> _vacationRepository;
        private readonly IMapper _mapper;

        public ApiVacationService(IApiRepository<Vacation> vacationRepository, IMapper mapper)
        {
            _vacationRepository = vacationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VacationDto>> GetVacationAsync()
        {
            var vacations = await _vacationRepository.GetAsync();

            var vacationDtos = _mapper.Map<IEnumerable<VacationDto>>(vacations);

            return vacationDtos.ToList();
        }

        public async Task<VacationDto> GetVacationByIdAsync(Guid id)
        {
            var vacation = await _vacationRepository.GetByIdAsync(id);

            var vacationDto = _mapper.Map<VacationDto>(vacation);

            return vacationDto;
        }

        public Task<bool> CreateVacationAsync(VacationDto entity)
        {
            var vacation = _mapper.Map<Vacation>(entity);

            return _vacationRepository.CreateAsync(vacation);
        }

        public Task<bool> UpdateVacationAsync(VacationDto entity)
        {
            var vacation = _mapper.Map<Vacation>(entity);

            return _vacationRepository.UpdateAsync(vacation);
        }

        public Task<bool> DeleteVacationByIdAsync(Guid id)
        {
            return _vacationRepository.DeleteAsync(id);
        }
    }
}
