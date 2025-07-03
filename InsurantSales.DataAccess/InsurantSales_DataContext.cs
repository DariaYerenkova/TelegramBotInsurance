using InsurantSales.DataAccess.EntityConfiguration;
using InsurantSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess
{
    public class InsurantSales_DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ExtractedField> ExtractedFields { get; set; }

        public InsurantSales_DataContext(DbContextOptions<InsurantSales_DataContext> options): base(options) { }

        //public InsurantSales_DataContext()
        //    : base(new DbContextOptionsBuilder<InsurantSales_DataContext>()
        //        .UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseConnectionString"),
        //            b => b.MigrationsAssembly("InsurantSales")).Options)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityConfigurations).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
