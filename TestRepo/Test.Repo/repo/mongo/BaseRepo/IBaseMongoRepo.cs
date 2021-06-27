using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test.Core.Model.BaseModel;

namespace Test.Repo.BaseRepo
{
    public interface IBaseMongoRepo<TDocument>
    {
        Task<IQueryable<TDocument>> AsQueryable();
        void DeleteById(string id);
        Task DeleteByIdAsync(string id);
        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task<IEnumerable<TProjected>> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
        Task<IEnumerable<TDocument>> FilterByExpression(Expression<Func<TDocument, bool>> filterExpression);
        Task<TDocument> FindById(string id);
        Task<TDocument> FindByIdAsync(string id);
        Task<TDocument> FindOne(Expression<Func<TDocument, bool>> filterExpression);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        void InsertMany(ICollection<TDocument> documents);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task InsertOne(TDocument document);
        Task<TDocument> InsertOneAsync(TDocument document);
        void ReplaceOne(TDocument document);
        Task ReplaceOneAsync(TDocument document);
    }
}