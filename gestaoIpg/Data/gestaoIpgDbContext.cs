using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gestaoIpg.Models;

    public class gestaoIpgDbContext : DbContext
    {
        public gestaoIpgDbContext (DbContextOptions<gestaoIpgDbContext> options)
            : base(options)
        {
        }

        public DbSet<gestaoIpg.Models.Departamento> Departamento { get; set; }

        public DbSet<gestaoIpg.Models.Funcionario> Funcionario { get; set; }

        public DbSet<gestaoIpg.Models.Cargo> Cargo { get; set; }

        public DbSet<gestaoIpg.Models.Tarefa> Tarefa { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        //Relação 1 -> N
        modelBuilder.Entity<Funcionario>()
            .HasOne(mm => mm.Cargo)
            .WithMany(m => m.Funcionarios)
            .HasForeignKey(mm => mm.CargoId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);


        //Relação 1 -> N
        modelBuilder.Entity<Funcionario>()
            .HasOne(mm => mm.Departamento)
            .WithMany(m => m.Funcionarios)
            .HasForeignKey(mm => mm.DepartamentoId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }


}

