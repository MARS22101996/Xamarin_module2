using AutoMapper;
using VTSClient.BLL.Dto;
using VTSClient.DAL.Entities;

namespace VTSClient.Core.Infrastructure.Automapper.Profiles
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<VacationDto, Vacation>();
        }
    }
}
