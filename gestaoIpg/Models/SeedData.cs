using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class SeedData
    {
        public static void Populate(gestaoIpgDbContext db)
        {
            PopulateDepartamento(db);
            PopulateFuncionario(db);
           
        }
        private static void PopulateDepartamento(gestaoIpgDbContext db)
        {
            if (db.Departamento.Any()) return;
            db.Departamento.AddRange(
                new Departamento { Tipo = "Departamento informática" },
                new Departamento { Tipo = "Departamento contabilidade" },
                new Departamento { Tipo = "Departamento gestao" },
                new Departamento { Tipo = "Departamento informática1" },
                new Departamento { Tipo = "Departamento contabilidade1" },
                new Departamento { Tipo = "Departamento gestao1" },
                new Departamento { Tipo = "Departamento informática2" },
                new Departamento { Tipo = "Departamento contabilidade2" },
                new Departamento { Tipo = "Departamento gestao2" }

            );
            db.SaveChanges();
        }

        private static void PopulateFuncionario(gestaoIpgDbContext db)
        {
            if (db.Funcionario.Any()) return;
            db.Funcionario.AddRange(
                new Funcionario { Nome = "Jorge Damásio", Morada = " Rua da Alegria, Guarda",
                    Email = "jorgedamásio@gmail.com",
                    Telemovel = 919191914
                },
                new Funcionario { Nome = "Daniela Matos", Morada = " Rua da Alegria, Guarda",
                    Email = "danielamatos@gmail.com",
                    Telemovel = 919191915
                },
                new Funcionario { Nome = "Olinda Simões", Morada = " Rua das Flores, Guarda",
                    Email = "olindasimoes@gmail.com",
                    Telemovel = 919191894
                }


            );
            db.SaveChanges();
        }
    }
}
