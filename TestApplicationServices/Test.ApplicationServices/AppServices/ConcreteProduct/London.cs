using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.ApplicationServices.AppServices.Product;

namespace Test.ApplicationServices.AppServices.ConcreteProduct
{
    class London : IWeatherForecastProvider
    {
        public string CityName => "London";
        public  string Url => "https://api.openweathermap.org/data/2.5/weather";
        public string ApiKey => "8785c0d6646c6fca3f42c04f4f86b95f";
        public Task<bool> GetWeatherForeCastForCity(string cityName)
        {
            var url = $"{Url}?q={cityName}&appid={ApiKey}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("connection", "keep-alive");
            request.AddHeader("Accept", "*/*");

            var response =  client.ExecuteAsync(request);
            using (var httpClient = new HttpClient())
            {
                var result =  httpClient.GetAsync(url);

            }
            if (response.Result.IsSuccessful)
            {
                return Task.FromResult(true);
            }
           return  Task.FromResult(false);
        }
    }
}
