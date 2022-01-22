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
    public class Lagos : IWeatherForecastProvider
    {
        public string CityName { get
            {
                return "Lagos";
            } }

        public string Url => "https://api.openweathermap.org/data/2.5/weather";
        public string ApiKey => "8785c0d6646c6fca3f42c04f4f86b95f";
        public async Task<bool> GetWeatherForeCastForCity(string cityName)
        {
            var url = $"{Url}?q={cityName}&appid={ApiKey}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("connection", "keep-alive");
            request.AddHeader("Accept", "*/*");
            HttpResponseMessage result = default;
            var response = await client.ExecuteAsync(request);
            using (var httpClient = new HttpClient())
            {
                 result = await httpClient.GetAsync(url);

            }

            if (result.IsSuccessStatusCode)
            {
                return (true);
            }
            return (false);
        }
    }
}
