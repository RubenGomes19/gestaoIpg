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
    }
}
