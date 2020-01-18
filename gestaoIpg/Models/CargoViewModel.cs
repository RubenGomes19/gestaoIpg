using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gestaoIpg.Models
{
    public class CargoViewModel <T> : List <T>
    {

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public CargoViewModel(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
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

        public static async Task<CargoViewModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new CargoViewModel<T>(items, count, pageIndex, pageSize);
        }


       /* public IEnumerable <Cargo> Cargo { get; set; }
         public int CurrentPage { get; set; }
         public int TotalPages { get; set; }
         public int FirstPageShow { get; set; }
         public int LastPageShow { get; set; }*/

    }
}
