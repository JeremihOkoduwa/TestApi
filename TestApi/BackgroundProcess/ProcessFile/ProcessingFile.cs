using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Test.Core;
using Test.Repo.BaseRepo;

namespace TestApi.BackgroundProcess.ProcessFile
{
    public class ProcessingFile : IProcessingFile
    {
        private const string Location = @"C:\Users\JEREMIAH\Desktop\New folder\worldcountries.csv";
        private readonly IBaseMongoRepository _baseMongoRepo;

        public ProcessingFile(IBaseMongoRepository baseMongoRepo)
        {
            _baseMongoRepo = baseMongoRepo;
        }

        public async Task<(bool, string)> ReadCsv()
        {
            bool result = false;
            string message = string.Empty;
            try
            {
                await Task.Run(() =>
                {
                    var timer = new Stopwatch();
                    timer.Start();

                    var allCountries = ReadFromFile();


                    Parallel.ForEach(allCountries, (item) =>
                    {

                        var result = _baseMongoRepo.InsertOne(item).Result;
                        Console.WriteLine($"{result.Nation} is inserted");
                        

                    });



                    result = true;
                    message = "Insertion completed";
                    timer.Stop();
                    Console.WriteLine($"elapsed time {timer.Elapsed}");
                }).ConfigureAwait(false);
                Console.WriteLine("Awaiting for task.run to finish");
            }
            catch (Exception ex)
            {

                result = false;
                message = ex.Message;
                return (result, message);
            }
            return (result, message);

        }

        IEnumerable<Country> ReadFromFile()
        {

            var file = File.ReadAllLines(Location).Skip(3).Select(x =>
           {
               var column = x.Split(",", StringSplitOptions.TrimEntries);
               return column;
           });

            foreach (var line in file)
            {

                yield return new Country
                {
                    Rank = line[0],
                    Nation = line[1],
                    Popuation = line[2],
                    Date = line[3],
                    PercentageOfWorldPopulation = line[3]
                };

            }
        }
    }
}
