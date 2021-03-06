using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.Model.BaseModel;

namespace Test.Core
{
    public class Author : BaseModels
    {
       
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        [BsonDefaultValue(false)]
        public bool? Ispublished { get; set; } 
        public string PhoneNumber { get; set; }
    }
}
