using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class DepartamentoViewModel
    {
        public IEnumerable<Departamento> Departamento { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int FirstPageShow { get; set; }
        public int LastPageShow { get; set; }
        public string CurrentSortOrder { get; set; }
    }
}
