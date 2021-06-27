using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Repo.BaseRepo;
using Test.Repo.repo.AuthorRepo;
using Test.Repo.repo.mongo;

namespace Test.Repo
{
    public static class ServiceCollectionRepo
    {
        public static IServiceCollection AddRepoServices(this IServiceCollection services)
        {
            //services.AddScoped<IMongoInit, MongoInit>();
            //services.AddTransient<IAuthorRepo, AuthorRepo>();
            
            //services.AddTransient(typeof(IBaseMongoRepo<>), typeof(BaseMongoRepository<>));

            return services;
        }
    }
}
