using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ApplicationServices.AppServices.Product;

namespace Test.ApplicationServices.AppServices.Manager
{
    public interface IWeatherForecastManager
    {
        Task<IWeatherForecastProvider> GetWeatherForecastProvider(string cityName);
    }
}
