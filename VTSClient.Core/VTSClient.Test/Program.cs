using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using VTSClient.BLL.Dto;
using VTSClient.BLL.Interfaces;

namespace VTSClient.Test
{
    class Program
    {
        private static ISqlVacationService _serviceSql;

        private static IApiVacationService _serviceApi;

        static void Main(string[] args)
        {

            SetUpContainers();

            var vacations = _serviceApi.GetVacationAsync().Result.ToList();

            ShowVacationsDto(vacations, "List of vacations Requests from the server.");

            CreateListOfVacationsInSql(vacations);

            vacations = _serviceSql.GetVacation().ToList();

            ShowVacationsDto(vacations, "List of vacations from the db after adding from the server.");

            var id = Guid.NewGuid();

            var newVacation = GenerateNewVacation(id);

            _serviceSql.CreateVacation(newVacation);

            _serviceApi.CreateVacationAsync(newVacation);

            vacations = _serviceSql.GetVacation().ToList();

            ShowVacationsDto(vacations, "List of vacations requests from the server after adding the new vacation.");

            var vacationsApi =  _serviceApi.GetVacationAsync().Result.ToList();

            ShowVacationsDto(vacationsApi, "List of vacations from the db after after adding the new vacation.");

        }

        private static void SetUpContainers()
        {
            ConsoleSetup.Initialize();

            _serviceSql = ConsoleSetup.Container.Resolve<ISqlVacationService>();

            _serviceApi = ConsoleSetup.Container.Resolve<IApiVacationService>();
        }

        private static void CreateListOfVacationsInSql(List<VacationDto> vacations)
        {
            foreach (var item in vacations)
            {
                if (_serviceSql.GetVacationById(item.Id) == null)
                {
                    _serviceSql.CreateVacation(item);
                }
            }
        }

        private static VacationDto GenerateNewVacation(Guid id)
        {
            var newVacation = new VacationDto
            {
                Id = id,
                CreateDate = DateTime.Now,
                CreatedBy = "test",
                End = DateTime.Now.AddDays(2),
                Start = DateTime.Now,
                VacationStatus = 1,
                VacationType = 1
            };
            return newVacation;
        }

        private static void ShowVacationsDto(List<VacationDto> vacations, string message)
        {
            if (vacations == null) throw new ArgumentNullException(nameof(vacations));
            Console.WriteLine(message);
            Console.WriteLine("-----------------------------------------------------------------------");
            foreach (var vacation in vacations)
            {
                Console.WriteLine($"Id {vacation.Id}" +
                                  $"Created date: {vacation.CreateDate:dd.MM.yyyy}," +
                                  $" Created by: {vacation.CreatedBy}," +
                                  $" Start:{vacation.Start:dd.MM.yyyy} , End {vacation.End:dd.MM.yyyy}, " +
                                  $" Vacation status: {vacation.VacationStatus}," +
                                  $" Vacation type: {vacation.VacationType}");
            }
        }
    }
}
