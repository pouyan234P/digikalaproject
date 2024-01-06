using digika_mobileapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IProductService:IBaseService
    {
        Task<T> SearchAsync<T>(string name);
        Task<T> GetProductbyIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(SetProductDTO setProductDTO);
    }
}
