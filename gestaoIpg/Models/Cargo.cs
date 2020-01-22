using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class Cargo
    {
        public int CargoId { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        [StringLength(248)]
        [Display(Name = "NomeCargo:")]
        public string NomeCargo { get; set; }

        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
