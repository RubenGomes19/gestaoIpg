using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Models
{
    public class SeedData
    {

        private const string ADMIN_ROLE = "admin";
        private const string MANAGER_ROLE = "manager";
        private const string FUNCIONARIO_ROLE = "funcionario";

        public static void Populate(gestaoIpgDbContext db)
        {
            PopulateDepartamento(db);
            PopulateCargo(db);
            PopulateFuncionario(db);
            PopulateTarefa(db);
           
        }
        private static void PopulateDepartamento(gestaoIpgDbContext db)
        {
            if (db.Departamento.Any()) return;
            db.Departamento.AddRange(
                new Departamento { Tipo = "Departamento Académico" },
                new Departamento { Tipo = "Departamento Administrativo" },
                new Departamento { Tipo = "Departamento Pedagógico" },
                new Departamento { Tipo = "Departamento Recursos Humanos" }

            );
            db.SaveChanges();
        }

        private static void PopulateCargo(gestaoIpgDbContext db)
        {
            if (db.Cargo.Any()) return;
            db.Cargo.AddRange(
                new Cargo { NomeCargo = "Diretor ESTG" },
                new Cargo { NomeCargo = "Secretária ESTG" },
                new Cargo { NomeCargo = "Professor" },
                new Cargo { NomeCargo = "Gestora RH" }
                
            );
            db.SaveChanges();
        }

        private static void PopulateFuncionario(gestaoIpgDbContext db)
        {
            if (db.Funcionario.Any()) return;
            db.Funcionario.AddRange(
                new Funcionario { Nome = "António Martins", Morada = " Rua da Alegria, Guarda",
                    Email = "antonio@ipg.com",
                    Telemovel = 919191914,
                    CargoId =4,
                    DepartamentoId=2
                   
                  
                },
                new Funcionario { Nome = "Daniela Matos", Morada = " Rua da Azeitona, Guarda",
                    Email = "danielamatos@ipg.com",
                    Telemovel = 919191915,
                    CargoId = 3,
                    DepartamentoId = 2
                },
                new Funcionario { Nome = "José Fonseca", Morada = " Praça do Comércio, Guarda",
                    Email = "jf@ipg.pt",
                    Telemovel = 919191894,
                    CargoId = 2,
                    DepartamentoId = 3
                },

                new Funcionario
                {
                    Nome = "Maria da Conceição",
                    Morada = " Rua das Flores, Guarda",
                    Email = "mc@ipg.pt",
                    Telemovel = 961386258,
                    CargoId = 1,
                    DepartamentoId = 4
                }

            );
            db.SaveChanges();
        }

        

            private static void PopulateTarefa(gestaoIpgDbContext db)
            {
                if (db.Tarefa.Any()) return;
                db.Tarefa.AddRange(
                    new Tarefa { DescricaoTarefa = "Assinar Calendário de Avaliações" , FuncionarioId = 4},
                    new Tarefa { DescricaoTarefa = "Vigiar teste Programação", FuncionarioId = 2 },
                    new Tarefa { DescricaoTarefa = "Fazer Calendário de ferias 2020", FuncionarioId = 3},
                    new Tarefa { DescricaoTarefa = "Contratar Professor de Artes", FuncionarioId = 1}

                );
                db.SaveChanges();

            }

        public static async Task PopulateUsersAsync(UserManager<IdentityUser> userManager)
        {
            const string ADMIN_USERNAME = "admin@ipg.pt";
            const string ADMIN_PASSWORD = "Secret123$";

            const string MANAGER_USERNAME = "antonio@ipg.pt";
            const string MANAGER_PASSWORD = "Secret123$";

            const string FUNCIONARIO_USERNAME = "jose@ipg.pt";
            const string FUNCIONARIO_PASSWORD = "Secret123$";

            IdentityUser user = await userManager.FindByNameAsync(ADMIN_USERNAME);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = ADMIN_USERNAME,
                    Email = ADMIN_USERNAME
                };

                await userManager.CreateAsync(user, ADMIN_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, ADMIN_ROLE))
            {
                await userManager.AddToRoleAsync(user, ADMIN_ROLE);
            }

            user = await userManager.FindByNameAsync(MANAGER_USERNAME);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = MANAGER_USERNAME,
                    Email = MANAGER_USERNAME
                };


                await userManager.CreateAsync(user, MANAGER_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, MANAGER_ROLE))
            {
                await userManager.AddToRoleAsync(user, MANAGER_ROLE);
            }

            user = await userManager.FindByNameAsync(FUNCIONARIO_USERNAME);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = FUNCIONARIO_USERNAME,
                    Email = FUNCIONARIO_USERNAME
                };

                await userManager.CreateAsync(user, FUNCIONARIO_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, FUNCIONARIO_ROLE))
            {
                await userManager.AddToRoleAsync(user, FUNCIONARIO_ROLE);
            }


            // Create other user accounts ...
        }

        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //const string CAN_ADD_MENUS = "can_add_menus";

            if (!await roleManager.RoleExistsAsync(ADMIN_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
            }

            if (!await roleManager.RoleExistsAsync(MANAGER_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(MANAGER_ROLE));
            }

            if (!await roleManager.RoleExistsAsync(FUNCIONARIO_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(FUNCIONARIO_ROLE));
            }
        }

    }
}
