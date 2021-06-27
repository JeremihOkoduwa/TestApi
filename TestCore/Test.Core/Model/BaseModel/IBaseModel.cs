using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Model.BaseModel
{
    public interface IBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
         string _id { get; set; }
    }
}
