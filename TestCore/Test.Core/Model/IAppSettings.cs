namespace Test.Core.Model
{
    public interface IAppSettings
    {
        string AuthorCollection { get; set; }
        string BooksCollection { get; set; }
        string ConnectionString { get; set; }
        string Databasename { get; set; }
    }
}