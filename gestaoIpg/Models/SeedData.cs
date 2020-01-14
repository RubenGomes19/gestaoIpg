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
            PopulateFuncionario(db);
            PopulateCargo(db);
           
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

        private static void PopulateCargo(gestaoIpgDbContext db)
        {
            if (db.Cargo.Any()) return;
            db.Cargo.AddRange(
                new Cargo { NomeCargo = "Presidente" },
                new Cargo { NomeCargo = "Vice-Presidente" },
                new Cargo { NomeCargo = "Gerente" },
                new Cargo { NomeCargo = "Analisador" },
                new Cargo { NomeCargo = "Tecnico" },
                new Cargo { NomeCargo = "Advogado" },
                new Cargo { NomeCargo = "Contabilista" },
                new Cargo { NomeCargo = "Limpeza" },
                new Cargo { NomeCargo = "Financeira" },
                new Cargo { NomeCargo = "Trabalhador1" },
                new Cargo { NomeCargo = "Trabalhador2" }
            );
            db.SaveChanges();

        }

        public static async Task PopulateUsersAsync(UserManager<IdentityUser> userManager)
        {
            const string ADMIN_USERNAME = "admin@ipg.pt";
            const string ADMIN_PASSWORD = "Secret123$";

            const string MANAGER_USERNAME = "jose@ipg.pt";
            const string MANAGER_PASSWORD = "Secret123$";

            const string FUNCIONARIO_USERNAME = "andre@ipg.pt";
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
