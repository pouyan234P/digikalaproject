
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Models
{
    public class Information
    {
        public readonly dynamic _data;

        public Information(dynamic data)
        {
            _data = data;
        }
        public Information()
        {

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
