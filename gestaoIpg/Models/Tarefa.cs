using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class Tarefa
    {

        public int TarefaId { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Descricao")]
        public string DescricaoTarefa { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }


    }
}
