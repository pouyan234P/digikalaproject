﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Models
{
    public class Products
    {
        public int id { get; set; }
        public int Informationid { get; set; }
        public Category Categoryid { get; set; }
        public string Name { get; set; }
        public string Insurance { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Nameforushghah { get; set; }
    }
}