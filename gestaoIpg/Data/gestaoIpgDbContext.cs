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
    }
