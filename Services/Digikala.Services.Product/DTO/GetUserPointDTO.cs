﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.DTO
{
    public class GetUserPointDTO
    {
        public int id { get; set; }
        public GetPointofviewDTO Pointofviewid { get; set; }
        public int Userid { get; set; }
        public ProductDTO Productid { get; set; }
    }
}
