using devboost.dronedelivery.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Repository.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Drone> Drone { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=DroneDelivery;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Drone>().HasKey(x => x.Id);

            builder.Entity<Pedido>().HasKey(x => x.Id);

            builder.Entity<Pedido>().HasOne(x => x.Drone)
                .WithMany(x => x.Pedidos)
                .HasForeignKey(x => x.DroneId);
        }
    }
}
