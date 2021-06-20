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
        private readonly AppSettings _appSettings;
        private static string connectionString;
        private IMongoDatabase db;
        public MongoInit(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
            connectionString = _appSettings.ConnectionString;
            
        }

        public async Task<IMongoCollection<T>> InitializeAuthorCollection<T>(string tableName) 
        {
           
            try
            {
                var client = new MongoClient(connectionString);
                db = client.GetDatabase(_appSettings.Databasename);
                var author = db.GetCollection<T>(tableName);
               
                return await Task.FromResult(author);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
