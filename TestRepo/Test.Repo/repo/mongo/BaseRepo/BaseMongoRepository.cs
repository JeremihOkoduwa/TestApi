using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Model.BaseModel;
using Test.Repo.repo.mongo;

namespace Test.Repo.BaseRepo
{
    public class BaseMongoRepository :  IBaseMongoRepository
    {
        private readonly IMongoDatabase db;
        private readonly IMongoInit _mongoDbInit;
        

        public BaseMongoRepository(IMongoInit mongoDbInit)
        {
            _mongoDbInit = mongoDbInit;
            db = _mongoDbInit.InitializeCollection().Result;



        }

        public async Task<IQueryable<TDocument>> AsQueryable<TDocument>()
        {
            try
            {
                return await Task.FromResult(db.GetCollection<TDocument>(typeof(TDocument).Name).AsQueryable());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TProjected>> FilterBy<TProjected, TDocument>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TDocument>> FilterByExpression<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            try
            {
                var listOfdocuments = await db.GetCollection<TDocument>(typeof(TDocument).Name).Find(filterExpression).ToListAsync();
                return listOfdocuments;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<TDocument> FindById<TDocument>(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindByIdAsync<TDocument>(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindOne<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<TDocument> FindOneAsync<TDocument>(Expression<Func<TDocument, bool>> filterExpression)
        {
            try
            {
                var result = await db.GetCollection<TDocument>(typeof(TDocument).Name).Find(filterExpression).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertMany<TDocument>(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync<TDocument>(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertOne<TDocument>(TDocument document)
        {
            throw new NotImplementedException();
        }

        public async Task<TDocument> InsertOneAsync<TDocument>(TDocument document)
        {
            try
            {
                await db.GetCollection<TDocument>(typeof(TDocument).Name).InsertOneAsync(document);
                return await Task.FromResult(document);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ReplaceOne<TDocument>(TDocument document)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceOneAsync<TDocument>(TDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
