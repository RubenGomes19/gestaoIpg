using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}
