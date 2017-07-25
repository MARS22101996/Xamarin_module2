﻿using System;
using System.Collections.Generic;
using VTSClient.BLL.Dto;

namespace VTSClient.BLL.Interfaces
{
    public interface ISqlVacationService
    {
        IEnumerable<VacationDto> GetVacation();

        VacationDto GetVacationById(Guid id);

        void CreateVacation(VacationDto entity);

        void UpdateVacation(VacationDto entity);

        void DeleteVacationById(Guid id);
    }
}
