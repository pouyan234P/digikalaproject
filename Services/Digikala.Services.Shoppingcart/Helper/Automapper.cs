using AutoMapper;
using Digikala.Services.Shoppingcart.DTO;
using Digikala.Services.Shoppingcart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Helper
{
    public class Automapper:Profile
    {
        public Automapper()
        {
            CreateMap<Cartdetail, CartDetailDTO>();
            CreateMap<CartHeader, CartHeaderDTO>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
