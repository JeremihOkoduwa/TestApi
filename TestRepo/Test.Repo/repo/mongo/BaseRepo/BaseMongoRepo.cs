using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Model.BaseModel;
using Test.Repo.BaseRepo;

namespace Test.Repo.repo.mongo.BaseRepo
{
    public class BaseMongoRepo<TDocument> : IBaseMongoRepo<TDocument> where TDocument : IBaseModel
    {
        private readonly IMongoDatabase db;
        private readonly IMongoInit _mongoDbInit;
        private readonly IMongoCollection<TDocument> _collection;

       
        public BaseMongoRepo(IMongoInit mongoDbInit)
        {
            _mongoDbInit = mongoDbInit;
            db = _mongoDbInit.InitializeCollection().Result;
            _collection = db.GetCollection<TDocument>(typeof(TDocument).Name);

        }

        public Task<IQueryable<TDocument>> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var result = _collection.DeleteOneAsync(filterExpression);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<TProjected>> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDocument>> FilterByExpression(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertOne(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> InsertOneAsync(TDocument document)
        {
            throw new NotImplementedException();
        }

        public void ReplaceOne(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceOneAsync(TDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
