﻿using AutoMapper;
using Digikala.Services.Product.DTO;
using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Helper
{
    public class AutoMapperHelper:Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<Products, ProductDTO>().ForMember(d => d.Categoryid, mapper => mapper.MapFrom(c => c.Categoryid.ID));
            //CreateMap<System.Collections.Generic.List<Products>, ProductDTO>().AfterMap<Convertfrombsondocumenttostringarray>();
        }
        
    }
    public class Convertfrombsondocumenttostring : IMappingAction<Products, ProductDTO>
    {
        public void Process(Products source, ProductDTO destination, ResolutionContext context)
        {
            byte[] bytes = source.pictures;
            var oneBigString = Encoding.ASCII.GetString(bytes);
            string[] lines = oneBigString.Split('\n');
            destination.pictures = lines;
           

        }
    }
    public class Convertfrombsondocumenttostringarray : IMappingAction<List<Products>,ProductDTO>
    {
        public void Process(List<Products> source,ProductDTO destination, ResolutionContext context)
        {
            int count = source.Count;
            while (count == 0)
            {
                byte[] bytes = source[count].pictures;
                var oneBigString = Encoding.ASCII.GetString(bytes);
                string?[] lines = oneBigString.Split('\n');
                destination.pictures = lines;
                count--;
            }


        }
    }
}
