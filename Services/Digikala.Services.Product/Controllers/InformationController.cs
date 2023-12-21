using Digikala.Services.Product.Data.Repository;
using Digikala.Services.Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IMongoInformationDBContext _mongoInformationDBContext;

        public InformationController(IMongoInformationDBContext mongoInformationDBContext)
        {
            _mongoInformationDBContext = mongoInformationDBContext;
        }
        [HttpPost("Addinformation")]
        public async Task<IActionResult> Addinformation([FromBody]dynamic jsonData)
        {
            var bsonDocument = BsonDocument.Parse(jsonData.ToString());
            var data =await _mongoInformationDBContext.Create(bsonDocument);
            return Ok(data);
        }
    }
}
