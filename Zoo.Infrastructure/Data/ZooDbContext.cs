using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Zoo.Core.Entities;
using Zoo.Infrastructure.Configuration;

namespace Zoo.Infrastructure.Data
{
    public class ZooDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public ZooDbContext(DbContextOptions<ZooDbContext> options)
             : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnimalConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
