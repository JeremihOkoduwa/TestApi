using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApi.BackgroundProcess.ProcessFile;

namespace TestApi.BackgroundProcess
{
    public class FileProcessor : BackgroundService
    {
        private readonly IProcessingFile _processingFile;

        public FileProcessor(IProcessingFile processingFile)
        {
            _processingFile = processingFile;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                
                while (!stoppingToken.IsCancellationRequested)
                {
                    //
                    Console.WriteLine($"Application Starting at {DateTimeOffset.Now}");
                    var result = await _processingFile.ReadCsv();
                    Console.WriteLine($"{result.Item2}");
                    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);

                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown {ex}");
                throw;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            //await Task.Delay(TimeSpan.FromMilliseconds(10));
            if (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"Task Canceled, stopping at {DateTime.Now}");
            }
        }
    }
}
