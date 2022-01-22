using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Test.ApplicationServices;
using Test.Core.Model;
using Test.Repo;
using Test.Repo.BaseRepo;
using Test.Repo.repo.AuthorRepo;
using Test.Repo.repo.mongo;
using Test.Repo.repo.mongo.BaseRepo;
using TestApi.BackgroundProcess;
using TestApi.BackgroundProcess.ProcessFile;
using TestApi.Middleware;

namespace TestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Configuration = configuration;
            var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();
            var index = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["ElasticSearch:Uri"]))
            {
                CustomFormatter = new ElasticsearchJsonFormatter(),
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            })
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();
            
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            
        

            services.AddSingleton<IProcessingFile, ProcessingFile>();
            services.AddHostedService<FileProcessor>();
            services.TryAddSingleton<IAppSettings>(x => x.GetRequiredService<IOptions<AppSettings>>().Value);
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddServiceLayerDependencies();
            services.AddTransient<IMongoInit, MongoInit>();
            services.AddTransient<IAuthorRepo, AuthorRepo>();

            //TryAddEnumerable used to remove duplication of registered services
            //services.TryAddEnumerable(ServiceDescriptor.Transient<IAuthorRepo, AuthorRepo>());
            services.AddTransient<IBaseMongoRepository, BaseMongoRepository>();
            services.AddScoped(typeof(IBaseMongoRepo<>), typeof(BaseMongoRepo<>));
            services.AddTransient<GuidService>();
            //services.AddTransient(typeof(IBaseMongoRepo<>), typeof(BaseMongoRepository<>));
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Cors-Policy", p =>
                {
                    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApi v1"));
            }
            app.UseMiddleware<CustomMiddleware>();
            app.UseCors(builder =>
                       builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
