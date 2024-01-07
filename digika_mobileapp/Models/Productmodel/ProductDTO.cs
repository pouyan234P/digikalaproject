﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Productmodel
{
    
    public class ProductDTO
    {
        public int id { get; set; }
        public object Informationid { get; set; }
        public int Categoryid { get; set; }
        public string Name { get; set; }
        public string Insurance { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string mainpictureUrlID { get; set; }
        public string[] pictures { get; set; }
        public string Nameforushghah { get; set; }
    }
}