using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Model
{
    public class AuthorDto
    {
        public AuthorDto()
        {
           
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        
        public bool? IsPublished { get; set; }
    }
}
