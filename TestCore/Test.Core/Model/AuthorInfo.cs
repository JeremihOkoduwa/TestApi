using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Model
{
    public class AuthorInfo
    {
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        public string EmailAddress { get; set; }
      
        public string PhoneNumber { get; set; }
    }
}
