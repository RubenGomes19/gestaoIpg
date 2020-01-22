using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gestaoIpg.Models
{
    public class FuncionarioViewModel
    {

        public IEnumerable<Funcionario> Funcionarios { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int FirstPageShow { get; set; }
        public int LastPageShow { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public string CurrentSortOrder { get; set; }
        public string CurrentSearchOption { get; set; }
        public string CurrentSearchString { get; set; }

        /*public  int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public FuncionarioViewModel(List<T> items, int count, int pageIndex, int pageSize)
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

        public static async Task<FuncionarioViewModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new FuncionarioViewModel<T>(items, count, pageIndex, pageSize);
        }
    }*/
    }
 }
