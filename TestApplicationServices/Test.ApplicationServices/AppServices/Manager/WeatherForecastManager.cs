using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ApplicationServices.AppServices.Product;

namespace Test.ApplicationServices.AppServices.Manager
{
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private readonly IEnumerable<IWeatherForecastProvider> _weatherForecastProviders;
        public WeatherForecastManager(IEnumerable<IWeatherForecastProvider> weatherForecastProviders) => _weatherForecastProviders = weatherForecastProviders;
        public Task<IWeatherForecastProvider> GetWeatherForecastProvider(string cityName)
        {
            try
            {
                var result = _weatherForecastProviders?.FirstOrDefault(x => x.CityName == cityName);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
