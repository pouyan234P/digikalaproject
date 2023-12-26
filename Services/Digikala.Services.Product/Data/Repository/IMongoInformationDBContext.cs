using Digikala.Services.Product.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
   public interface IMongoInformationDBContext
    {
      public Task<BsonDocument> Create(BsonDocument data);
      public Task<BsonDocument> Getlastdata();
        public Task<BsonDocument> GetInformation(string id);
    }
}
