using Microsoft.EntityFrameworkCore;
using WebApiDDD.Domain.Models;

namespace WebApiDDD.Infra.Data.Context
{
    public class WebApiDDDContext : DbContext
    {
        public WebApiDDDContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Carros> Carros { get; set; }
        public DbSet<Marcas> Marcas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebApiDDDContext).Assembly);
        }
    }
}
