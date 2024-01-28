using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Helper
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int pageSize = 10;
      
    }
}
