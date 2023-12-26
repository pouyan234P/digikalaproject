using Digikala.Services.Product.Helper;
using Digikala.Services.Product.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Digikala.Services.Product.Data.Repository
{
    public class MongoInformationDBContex : IMongoInformationDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public MongoInformationDBContex(IOptions<Mongosettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.Connection);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public async Task<BsonDocument> Create(BsonDocument data)
        {
            var collection = _db.GetCollection<BsonDocument>("myNewCollection");
             await collection.InsertOneAsync(data);
            return await collection.Find(data).FirstOrDefaultAsync();
        }

        public async Task<BsonDocument> Getlastdata()
        {
            var collection = _db.GetCollection<BsonDocument>("myNewCollection");
            var filter = new BsonDocument("_id", ObjectId.Parse("65873a4228110df09f5404b7"));
            var lastData =await  collection.Find(filter).FirstOrDefaultAsync();
            return lastData;
        }
    }
}
