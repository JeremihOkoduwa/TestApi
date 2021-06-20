using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Repo.repo.mongo
{
    public interface IMongoInit
    {
        Task<IMongoCollection<T>> InitializeAuthorCollection<T>(string tableName);
    }
}
