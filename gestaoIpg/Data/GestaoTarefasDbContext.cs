using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestaoIpg.Data
{
    public class GestaoTarefasDbContext : DbContext
    {
        public GestaoTarefasDbContext(DbContextOptions<GestaoTarefasDbContext> options)
            : base(options)
        {
        }
    }
}
