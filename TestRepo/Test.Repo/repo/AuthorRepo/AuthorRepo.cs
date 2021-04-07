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

namespace Test.Repo.repo.AuthorRepo
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly IMongoCollection<Author> _author;
        private readonly IMongoInit _mongoDbInit;

        public AuthorRepo(IMongoInit mongoDbInit)
        {
            _mongoDbInit = mongoDbInit;
            _author = mongoDbInit.InitializeAuthorCollection().Result;
        }

        public Task<List<AuthorInfo>> GetAllAuthorInfo()
        {
            IEnumerable<Author> authors = new List<Author>();
            var allAuthors = _author.Find(x => true).ToList();
           var allAuthorInfo = allAuthors.Where(x => x.Ispublished == true).Select(authInfo => new AuthorInfo
            {
                Address = authInfo.Address,
                PhoneNumber = authInfo.PhoneNumber,
                EmailAddress = authInfo.EmailAddress,
                Name = $"{authInfo.FirstName} {authInfo.LastName}"
            });

            return Task.FromResult(allAuthorInfo.ToList());
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
                var filter = Builders<Author>.Filter.Where(x => x.FirstName == model.FirstName && x.LastName == model.LastName);
                var result =  _author.Find(filter).ToList();
                if (result.Count > 0)
                {
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
                await _author.InsertOneAsync(obj);
                return (true, obj._id);
            }
            catch (Exception ex)
            {


                return (false, ex.Message);

            }

        }
        public async Task<Author> GetbyAuthorId(int authorId)
        {
            var filter = Builders<Author>.Filter.Eq(x => x.AuthorId, authorId);

            var result = await _author.Find(filter).FirstOrDefaultAsync();

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
