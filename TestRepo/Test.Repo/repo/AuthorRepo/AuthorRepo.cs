using MimeKit;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;
using Test.Core.Model;
using Test.Repo.repo.mongo;
using Test.Repo.BaseRepo;
using System.Linq.Expressions;
using System.Threading;

namespace Test.Repo.repo.AuthorRepo
{
    public class AuthorRepo : IAuthorRepo
    {
        private const string tableName = "Author";
        private readonly IMongoCollection<Author> _author;
        private readonly IMongoInit _mongoDbInit;
        private readonly IBaseMongoRepository _baseMongoRepo;
        private readonly IBaseMongoRepo<Author> _baseRepo;

        public AuthorRepo(IBaseMongoRepository baseMongoRepo, IBaseMongoRepo<Author> baseRepo)
        {
            _baseMongoRepo = baseMongoRepo;
            _baseRepo = baseRepo;
        }

        public async Task<List<Author>> Search(string search)
        {
            var modifiedSearch = search;
            var modifiedChar = search.ToCharArray();
            
            List<Author> result = null;
            List<char> modSearch = new List<char>();
            for (int i = 0; i < search.Length; i++)
            {
                if (modifiedChar[i] != '?')
                {
                    modSearch.Add(modifiedChar[i]);
                    
                }
            }

            var currentString = new string(modSearch.ToArray());

            //_author.Indexes.CreateOne(new CreateIndexModel<Author>(Builders<Author>.IndexKeys.Text(x => x.FirstName)));
            var results = await _baseMongoRepo.AsQueryable<Author>();
            var res = results.Where(x => x.FirstName.ToLower().Contains(currentString.ToLower())).ToList(); 
            //results.where
            result = _author.Find(Builders<Author>.Filter.Text(search)).ToList();
            //await Task.Run(() =>
            //{
            //    _author.Indexes.CreateOne(new CreateIndexModel<Author>(Builders<Author>.IndexKeys.Text("$**")));
            //     result = _author.Find(Builders<Author>.Filter.Text(search)).ToList();

            //});

            return res;
           
                
        }

        
        public async Task<List<AuthorInfo>> GetAllAuthorInfo(IProgress<List<AuthorInfo>> progress)
        {
            IEnumerable<Author> authors = new List<Author>();
            TaskCompletionSource<List<Author>> tsc = default;
            Expression<Func<Author, bool>> expression = x => true;
            

           //var taskCompletion =  await TaskCompletionOnThreadPool(tsc, expression);

            var allAuthors = await _baseMongoRepo.FilterByExpression(expression);
            var allAuthorInfo = allAuthors.Where(x => x.Ispublished == true).Select(authInfo => new AuthorInfo
            {
                Address = authInfo.Address,
                PhoneNumber = authInfo.PhoneNumber,
                EmailAddress = authInfo.EmailAddress,
                Name = $"{authInfo.FirstName} {authInfo.LastName}"
            });
            progress.Report(allAuthorInfo.ToList());
            return allAuthorInfo.ToList();
            //var allAuthorInfo = from authorInfo in allAuthors
            //                    where authorInfo.Ispublished == true
            //                    select new AuthorInfo
            //                    {
            //                        Address = authorInfo.Address,
            //                        EmailAddress = authorInfo.EmailAddress,
            //                        Name = $"{authorInfo.FirstName} {authorInfo.LastName}"

            //                    };
        }

        /// <summary>
        /// use Task completion Source to create awaitables out of legace codes that don't use TPL
        /// </summary>
        /// <param name="tsc"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private Task<List<Author>> TaskCompletionOnThreadPool( TaskCompletionSource<List<Author>> tsc,  Expression<Func<Author, bool>> expression)
        {
            tsc = new TaskCompletionSource<List<Author>>(TaskCreationOptions.RunContinuationsAsynchronously);
            expression = x => true;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var allAuthors = _baseMongoRepo.FilterByExpression(expression);
                tsc.SetResult(allAuthors.Result.ToList());

            });
            return tsc.Task;
        }

        public async IAsyncEnumerable<AuthorInfo> GetAuthorStreams()
        {
            
            IEnumerable<Author> authors = new List<Author>();
            Expression<Func<Author, bool>> expression = x => true;
            
            var allAuthors = await _baseMongoRepo.FilterByExpression(expression);
            //remove code;
            await Task.Delay(5000, default);
            foreach (var item in allAuthors.ToList().Where(x => x.Ispublished == true))
            {
                //remove code
                await Task.Delay(5000, default);
                yield return new   AuthorInfo
                {
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    EmailAddress = item.EmailAddress,
                    Name = $"{item.FirstName} {item.LastName}"
                };
            }
            

            

            
            //var allAuthorInfo = from authorInfo in allAuthors
            //                    where authorInfo.Ispublished == true
            //                    select new AuthorInfo
            //                    {
            //                        Address = authorInfo.Address,
            //                        EmailAddress = authorInfo.EmailAddress,
            //                        Name = $"{authorInfo.FirstName} {authorInfo.LastName}"

            //                    };
        }
        public async Task<(bool, string)> Insert(AuthorDto model)
        {
            
            Random rd = new Random();
            try
            {
                Expression<Func<Author, bool>> expression = x => x.FirstName == model.FirstName && x.LastName == model.LastName;

                var result = await _baseMongoRepo.FilterByExpression(expression);
                if (result.Count() > 0)
                {
                    //foreach (var item in result)
                    //{
                    //   var isDeleted = await  Delete(item.AuthorId);
                    //}
                   
                   return (false, "Author Exists, insert new Author");
                }
                var obj = new Author
                {
                    Address = model.Address,
                    AuthorId = rd.Next(90, 1000),
                    EmailAddress = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Ispublished = model.IsPublished,
                    
                    PhoneNumber = model.PhoneNumber
                    
                };
                var auhorInserted = await _baseMongoRepo.InsertOneAsync<Author>(obj);
                return (true, auhorInserted._id);
            }
            catch (Exception ex)
            {


                return (false, ex.Message);

            }

        }
        private Task<bool> Delete(int id)
        {
            try
            {
                Expression<Func<Author, bool>> expression = x => x.AuthorId == id;
               var result = _baseRepo.DeleteOneAsync(expression);
                return Task.FromResult(result.IsCompletedSuccessfully);
                //return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Author> GetbyAuthorId(int authorId)
        {
            Expression<Func<Author, bool>> expression = x => x.AuthorId == authorId;

            var result = await _baseMongoRepo.FindOneAsync(filterExpression: expression);

            return result;

        }
        
        public MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }
    }
}
