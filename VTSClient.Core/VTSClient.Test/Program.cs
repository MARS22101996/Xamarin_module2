using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using VTSClient.BLL.Dto;
using VTSClient.BLL.Interfaces;
using VTSClient.Core.Infrastructure.Automapper;
using VTSClient.Test.Infrastructure;

namespace VTSClient.Test
{
    class Program
    {
        private static ISqlVacationService _serviceSql;

        private static IApiVacationService _serviceApi;

        private static IEnumerable<VacationDto> _vacations;

        static void Main(string[] args)
        {
            SetUpContainers();

            RetrievesVacationsFromTheServerAndStoresInTheDb();

            StoresVacationInTheDbAndUploadstoTheServer();
        }

        private static void SetUpContainers()
        {
            ConsoleSetup.Initialize();

            _serviceSql = ConsoleSetup.Container.Resolve<ISqlVacationService>();

            _serviceApi = ConsoleSetup.Container.Resolve<IApiVacationService>();
        }

        private static void RetrievesVacationsFromTheServerAndStoresInTheDb()
        {
            _vacations = _serviceApi.GetVacationAsync().Result.ToList();

            ShowVacationsDto(_vacations, "List of vacations Requests from the server.");

            CreateListOfVacationsInSql(_vacations);

            _vacations = _serviceSql.GetVacation();

            ShowVacationsDto(_vacations, "List of vacations from the db after adding from the server.");
        }

        private static void StoresVacationInTheDbAndUploadstoTheServer()
        {
            var id = Guid.NewGuid();

            var newVacation = GenerateNewVacation(id);

            _serviceSql.CreateVacation(newVacation);

            _serviceApi.CreateVacationAsync(newVacation);

            _vacations = _serviceApi.GetVacationAsync().Result;

            ShowVacationsDto(_vacations, "List of vacations requests from the server after adding the new vacation.");

            _vacations = _serviceSql.GetVacation().ToList();

            ShowVacationsDto(_vacations, "List of vacations from the db after after adding the new vacation.");
        }

        private static void CreateListOfVacationsInSql(IEnumerable<VacationDto> vacations)
        {
            var existedVacations = _serviceSql.GetVacation().ToList();
            foreach (var item in vacations)
            {
                if (existedVacations.FirstOrDefault(x => x.Id == item.Id)==null)
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
                CreatedBy = "new-vacation",
                End = DateTime.Now.AddDays(2),
                Start = DateTime.Now,
                VacationStatus = 4,
                VacationType = 4
            };
            return newVacation;
        }

        private static void ShowVacationsDto(IEnumerable<VacationDto> vacations, string message)
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
