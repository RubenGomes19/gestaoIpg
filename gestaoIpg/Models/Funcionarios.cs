using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class Funcionarios
    {
        
        public int IdFuncionario { get; set;}

        [Required]
        [StringLength(256)]
        public String NomeFuncionario { get; set; }

    }
}
