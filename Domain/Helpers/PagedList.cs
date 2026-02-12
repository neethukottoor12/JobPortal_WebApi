using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class PagedList<T>:List<T>
    {
        private object count;
        private int pagenumber;

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PagedList() { }
        public PagedList(IEnumerable<T> items,int count,int pagenumber,int pagesize)
        {
            CurrentPage = pagenumber;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            PageSize = pagesize;
            TotalCount = count;
            AddRange(items);
        }
        public PagedList(object count, int pageNumber, int pageSize)
        {

            this.count = count;
            this.pagenumber = pageNumber;
            PageSize = pageSize;
        }
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
