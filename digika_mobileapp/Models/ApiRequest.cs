using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static digika_mobileapp.SD;

namespace digika_mobileapp.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
        public IFormFile file { get; set; }
        public IList<IFormFile> Ifile { get; set; }
    }
}
