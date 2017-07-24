using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Interfaces;
using VTSClient.DAL.Repositories;

namespace VTSClient.Test
{
    class Program
    {
        private static ISqlRepositoryVacation repo;

        static void Main(string[] args)
        {
            ConsoleSetup.Initialize();

            repo = ConsoleSetup.Container.Resolve<ISqlRepositoryVacation>();

            var testId = Guid.NewGuid();

            var newVacation = new Vacation
            {
                Id = testId,
                CreateDate = DateTime.Now,
                CreatedBy = "someone",
                End = DateTime.Now.AddDays(2),
                Start = DateTime.Now,
                VacationStatus = 1,
                VacationType = 1
            };

            repo.Create(newVacation);

            var item = repo.GetById(newVacation.Id);

            ShowVacation(item);

            var vacations =  repo.GetAll();

            ShowVacations(vacations.ToList());

            newVacation.VacationStatus = 2;

            repo.Update(newVacation);

            vacations = repo.GetAll();

            ShowVacations(vacations.ToList());

            repo.Delete(newVacation.Id);

            vacations = repo.GetAll();

            ShowVacations(vacations.ToList());

            //GetAndShowVacations();
        }

        private static async void GetAndShowVacations()
        {
            var repo = new ApiRepositoryVacation();
            var vacationDtos = await repo.GetAsync();
            var id = vacationDtos.ToList().First().Id;
            var vacation = await repo.GetByIdAsync(id);
            ShowVacations(vacationDtos.ToList());

            var testId = Guid.NewGuid();

            ShowVacation(vacation);

            var newVacation = new Vacation
            {
                Id = testId,
                CreateDate = DateTime.Now,
                CreatedBy = "someone",
                End = DateTime.Now.AddDays(2),
                Start = DateTime.Now,
                VacationStatus = 1,
                VacationType = 1
            };

            await repo.CreateAsync(newVacation);

            var vacationAfterCreate = await repo.GetByIdAsync(testId);

            ShowVacation(vacationAfterCreate);

            var updateVacation = new Vacation
            {
                Id = testId,
                CreateDate = DateTime.Now,
                CreatedBy = "someone1",
                End = DateTime.Now.AddDays(2),
                Start = DateTime.Now,
                VacationStatus = 1,
                VacationType = 1
            };

            await repo.UpdateAsync(updateVacation);

            var vacationAfterUpdate = await repo.GetByIdAsync(testId);

            ShowVacation(vacationAfterUpdate);

            await repo.UpdateAsync(updateVacation);

            var vacationsAfterUpdate = await repo.GetAsync();

            ShowVacations(vacationsAfterUpdate.ToList());

            await repo.DeleteAsync(testId);

            var vacationsAfterDelete = await repo.GetAsync();

            ShowVacations(vacationsAfterDelete.ToList());
        }

        private static void ShowVacations(List<Vacation> vacations)
        {
            if (vacations == null) throw new ArgumentNullException(nameof(vacations));
            Console.WriteLine("///////////");
            foreach (var vacation in vacations)
            {
                Console.WriteLine($"Id {vacation.Id}" +
                                  $"Created date: {vacation.CreateDate.ToString("dd.MM.yyyy")}," +
                                  $" Created by: {vacation.CreatedBy}," +
                                  $" Start:{vacation.Start:dd.MM.yyyy} , End {vacation.End:dd.MM.yyyy}, " +
                                  $" Vacation status: {vacation.VacationStatus}," +
                                  $" Vacation type: {vacation.VacationType}");
            }
        }

        private static void ShowVacation(Vacation vacation)
        {
            Console.WriteLine("///////////");
            Console.WriteLine($"Id {vacation.Id}" +
                              $"Created date: {vacation.CreateDate.ToString("dd.MM.yyyy")}," +
                              $" Created by: {vacation.CreatedBy}," +
                              $" Start:{vacation.Start:dd.MM.yyyy} , End {vacation.End:dd.MM.yyyy}, " +
                              $" Vacation status: {vacation.VacationStatus}," +
                              $" Vacation type: {vacation.VacationType}");
        }

        //public string GetDatabasePath(string fileName)
        //{
        //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    string path = Path.Combine(documentsPath, fileName);

        //    return path;
        //}
    }
}
