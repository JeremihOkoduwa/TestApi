using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test.Repo.BaseRepo
{
    public interface IBaseMongoRepository
    {
        Task<IQueryable<TDocument>> AsQueryable<TDocument>();
        void DeleteById(string id);
        Task DeleteByIdAsync(string id);
        void DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        void DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        Task<IEnumerable<TProjected>> FilterBy<TProjected, TDocument>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
        Task<IEnumerable<TDocument>> FilterByExpression<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        Task<TDocument> FindById<TDocument>(string id);
        Task<TDocument> FindByIdAsync<TDocument>(string id);
        Task<TDocument> FindOne<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        Task<TDocument> FindOneAsync<TDocument>(Expression<Func<TDocument, bool>> filterExpression);
        void InsertMany<TDocument>(ICollection<TDocument> documents);
        Task InsertManyAsync<TDocument>(ICollection<TDocument> documents);
        Task<TDocument> InsertOne<TDocument>(TDocument document);
        Task<TDocument> InsertOneAsync<TDocument>(TDocument document);
        void ReplaceOne<TDocument>(TDocument document);
        Task ReplaceOneAsync<TDocument>(TDocument document);
    }
}