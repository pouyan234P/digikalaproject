﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Shoppingmodel
{
    public class ProductDTO
    {
        public int id { get; set; }
        public int productid { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public byte[] Picture { get; set; }
    }
}
