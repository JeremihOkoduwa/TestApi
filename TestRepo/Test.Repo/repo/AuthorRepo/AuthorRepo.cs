using MimeKit;
using MongoDB.Driver;
using System;
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

        public async Task<(bool, string)> Insert(AuthorDto model)
        {
            Random rd = new Random();
            try
            {
                var obj = new Author
                {
                    Address = model.Address,
                    AuthorId = rd.Next(90, 1000),
                    EmailAddress = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
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
