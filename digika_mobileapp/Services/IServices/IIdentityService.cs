using digika_mobileapp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IIdentityService
    {
        Task<T> Register<T>(RegisterDTO registerDTO);
        Task<T> Login<T>(UserForLoginDTO userForLoginDTO);
    }
}
