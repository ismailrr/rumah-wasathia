using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRWV2
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageIndex2 { get; private set; }
        public int PageIndex3 { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalPages2 { get; private set; }
        public int TotalPages3 { get; private set; }
        public int TotalItem { get; private set; }
        public int TotalTrash { get; private set; }
        public int FirstPage { get; private set; }
        public int LastPage { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize,int totalItem, int totalTrash)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItem = totalItem;
            TotalTrash = totalTrash;
            FirstPage = 1;
            LastPage = TotalPages;

            this.AddRange(items);
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItem = count;
            FirstPage = 1;
            LastPage = TotalPages;

            this.AddRange(items);
        }

        public PaginatedList(List<T> items1, List<T> items2, List<T> items3, int count1, int count2, int count3, int pageIndex1, int pageIndex2, int pageIndex3, int pageSize1, int pageSize2)
        {
            PageIndex = pageIndex1;
            PageIndex2 = pageIndex2;
            PageIndex3 = pageIndex3;
            TotalPages = (int)Math.Ceiling(count1 / (double)pageSize1);
            TotalPages2 = (int)Math.Ceiling(count2 / (double)pageSize2);
            TotalPages3 = (int)Math.Ceiling(count3 / (double)pageSize2);
            FirstPage = 1;
            LastPage = TotalPages;

            this.AddRange(items1);
            this.AddRange(items2);
            this.AddRange(items3);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, int totalItem, int totalTrash)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, totalItem, totalTrash);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source1, IQueryable<T> source2, IQueryable<T> source3, int pageIndex1, int pageIndex2, int pageIndex3, int pageSize1, int pageSize2)
        {
            var count1 = await source1.CountAsync();
            var count2 = await source2.CountAsync();
            var count3 = await source3.CountAsync();

            var items1 = await source1.Skip((pageIndex1 - 1) * pageSize1).Take(pageSize1).ToListAsync();
            var items2 = await source2.Skip((pageIndex2 - 1) * pageSize2).Take(pageSize2).ToListAsync();
            var items3 = await source3.Skip((pageIndex3 - 1) * pageSize2).Take(pageSize2).ToListAsync();

            return new PaginatedList<T>(items1, items2, items3, count1, count2, count3, pageIndex1, pageIndex2, pageIndex3, pageSize1, pageSize2);
        }
    }
}