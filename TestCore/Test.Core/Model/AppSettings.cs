using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Model
{
    public class AppSettings : IAppSettings
    {
        public string Databasename { get; set; }
        public string ConnectionString { get; set; }
        public string BooksCollection { get; set; }
        public string AuthorCollection { get; set; }
    }
}
