using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Digikala.Services.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.Helper
{
    public class Pagedlist<T>:List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public Pagedlist(List<T> items,int count,int pageNumber,int PageSize)
        {
            TotalCount = Count;
            this.PageSize = PageSize;
            CurrentPage = pageNumber;
            TotalPage = (int)Math.Ceiling(count / (double)PageSize);
            this.AddRange(items);
        }
        public static async Task<Pagedlist<T>> CreateAsync(IQueryable<T> source,int pageNumber,int PageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            return new Pagedlist<T>(items, count, pageNumber, PageSize);
        }

        //internal static async Task<Pagedlist<Bought>> CreatesAsync(IIncludableQueryable<Bought, User> bought, int pageNumber, int pageSize)
        //{
        //    var count = await bought.CountAsync();
        //    var items = await bought.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //    return new Pagedlist<Bought>(items, count, pageNumber, pageSize);
        //}
    }
}
