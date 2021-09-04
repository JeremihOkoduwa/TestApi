using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Core;
using Test.Core.Model;

namespace Test.Repo.repo.AuthorRepo
{
    public interface IAuthorRepo
    {
        Task<(bool, string)> Insert(AuthorDto model);
        Task<List<Author>> Search(string search);
        Task<Author> GetbyAuthorId(int authorId);
        Task<List<AuthorInfo>> GetAllAuthorInfo(IProgress<List<AuthorInfo>> progress);
        MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message);
    }
}