using MetersSender.Common.Models;
using MetersSender.DataAccess.Database.Configurations;
using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MetersSender.DataAccess.Database
{
    public class MetersSenderDbContext : DbContext
    {
        private readonly string _connectionString;
        public MetersSenderDbContext(IOptionsMonitor<ServiceSettings> options)
        {
            _connectionString = options.CurrentValue.DbConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceEntityTypeConfiguration).Assembly);
        }

        public DbSet<Service> Services { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<Meter> Meters { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Reading> Readings { get; set; }
    }
}
