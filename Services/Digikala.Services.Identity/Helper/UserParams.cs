using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.Helper
{
    public class UserParams
    {
        public const int MaxPage = 50;
        public int PageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;

    }
}
