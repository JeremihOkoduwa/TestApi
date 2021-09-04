using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Core.Model;
using Test.Repo.repo.mongo;

namespace TestApi.Middleware
{
    public class CustomMiddleware
    {
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly RequestDelegate _next;
       

        public CustomMiddleware(ILogger<CustomMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context, GuidService guidService)
        {
            var logMessage = $"guidvalue: \"{guidService.GetGuidService()}\"";
            //We need to read the response stream from the beginning...

            var obj = context.Request.Body;
            await using var ms = new MemoryStream();
            var type = typeof(AuthorDto);
            ms.Position = 0;
            var stringJson =  await new StreamReader(context.Request.Body).ReadToEndAsync();
            var json = await JsonSerializer.DeserializeAsync<AuthorDto>(ms, type);
            await JsonSerializer.SerializeAsync(ms, new AuthorDto(), type);
            context.Items.Add("MiddlewareGuid", logMessage);
            _logger.LogInformation("result =>", stringJson);
            _logger.LogInformation(logMessage);
            await _next(context);
        }

        
      
    }

   
}
