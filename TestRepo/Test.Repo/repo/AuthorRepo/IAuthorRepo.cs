using MimeKit;
using System.Threading.Tasks;
using Test.Core;
using Test.Core.Model;

namespace Test.Repo.repo.AuthorRepo
{
    public interface IAuthorRepo
    {
        Task<(bool, string)> Insert(AuthorDto model);
        Task<Author> GetbyAuthorId(int authorId);
        MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message);
    }
}