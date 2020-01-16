using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class Funcionario
    {

        public int FuncionarioId { get; set; }

        [Required]
        [StringLength(256)]
        public String Nome {get; set;}

        [Required]
        [StringLength(256)]
        public String Morada { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public int Telemovel { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

    }
}
