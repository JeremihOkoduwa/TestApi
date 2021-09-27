using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestApi.BackgroundProcess
{
    public class FileProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                
                while (!stoppingToken.IsCancellationRequested)
                {
                    //
                    Console.WriteLine($"Application Starting at {DateTimeOffset.Now}");
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
