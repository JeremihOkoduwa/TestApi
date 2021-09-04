using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;
using Test.Core.Model;

namespace Test.Repo.repo.mongo
{
    public class MongoInit : IMongoInit
    {
        //private IMongoCollection<T> author;
        private readonly IAppSettings _appSettings;
        private static string connectionString;
        private IMongoDatabase db;
        public MongoInit(IAppSettings options)
        {
            _appSettings = options;
            connectionString = options.ConnectionString;
            
        }

        public async Task<IMongoDatabase> InitializeCollection() 
        {
           
            try
            {
                var client = new MongoClient(connectionString);
                db = client.GetDatabase(_appSettings.Databasename);
                //var author = db.GetCollection<T>(nameof(T));
               
                return await Task.FromResult(db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
