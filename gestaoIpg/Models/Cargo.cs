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

        [Required]
        [StringLength(100)]
        public string NomeCargo { get; set; }

    }
}
