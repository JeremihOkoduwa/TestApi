using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Model.BaseModel
{
    public abstract class BaseModels : IBaseModel
    {
        protected BaseModels()
        {
            DateCreated = DateTime.Now;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
