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
        private IMongoCollection<Author> author;
        private readonly AppSettings _appSettings;
        private static string connectionString;
        private IMongoDatabase db;
        public MongoInit(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
            connectionString = _appSettings.ConnectionString;
            
        }

        public async Task<IMongoCollection<Author>> InitializeAuthorCollection()
        {
           
            try
            {
                var client = new MongoClient(connectionString);
                db = client.GetDatabase(_appSettings.Databasename);
                author = db.GetCollection<Author>(_appSettings.AuthorCollection);
                return await Task.FromResult(author);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
