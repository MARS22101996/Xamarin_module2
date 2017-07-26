using System;
using System.Collections.Generic;
using AutoMapper;
using VTSClient.BLL.Dto;
using VTSClient.BLL.Interfaces;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Interfaces;

namespace VTSClient.BLL.Services
{
    public class SqlVacationService : ISqlVacationService
    {
        private readonly ISqlRepositoryVacation _vacationRepository;
        private readonly IMapper _mapper;

        public SqlVacationService(ISqlRepositoryVacation vacationRepository, IMapper mapper)
        {
            _vacationRepository = vacationRepository;
            _mapper = mapper;
        }

        public IEnumerable<VacationDto> GetVacation()
        {
            var vacations = _vacationRepository.GetAll();

            var vacationDtos = _mapper.Map<IEnumerable<VacationDto>>(vacations);

            return vacationDtos;
        }

        public VacationDto GetVacationById(Guid id)
        {
            var vacation = _vacationRepository.GetById(id);

            var vacationDto = _mapper.Map<VacationDto>(vacation);

            return vacationDto;
        }

        public void CreateVacation(VacationDto entity)
        {
            var vacation = _mapper.Map<Vacation>(entity);

            _vacationRepository.Create(vacation);
        }

        public void UpdateVacation(VacationDto entity)
        {
            var vacation = _mapper.Map<Vacation>(entity);

            _vacationRepository.Update(vacation);
        }

        public void DeleteVacationById(Guid id)
        {
            _vacationRepository.Delete(id);
        }
    }
}
