using digika_mobileapp.Helper;
using digika_mobileapp.Models.Productmodel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IProductService:IBaseService
    {
        Task<T> SearchAsync<T>(string name,UserParams userParams);
        Task<T> GetProductbyIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(SetProductDTO setProductDTO);
        Task<T> SendpictureAsync<T>( IFormFile mainpicture);
        Task<T> SendseconpictureAsync<T>(IList<IFormFile> mypictures);
    }
}
