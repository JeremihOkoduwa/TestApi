using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ApplicationServices.AppServices.Product
{
    public interface IWeatherForecastProvider
    {
        string CityName { get; }
        Task<bool> GetWeatherForeCastForCity(string cityName);
    }
}
