using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ApplicationServices.AppServices.ConcreteProduct;
using Test.ApplicationServices.AppServices.Manager;
using Test.ApplicationServices.AppServices.Product;

namespace Test.ApplicationServices
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceLayerDependencies(this IServiceCollection service)
        {
            service.AddTransient<IWeatherForecastProvider, Lagos>();
            service.AddTransient<IWeatherForecastProvider, London>();
            service.AddTransient<IWeatherForecastManager, WeatherForecastManager>();

            return service;
        }
    }
}
