using DogsCompanion.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Data
{
    public class DogsCompanionContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Dog> Dogs => Set<Dog>();
        public DbSet<VetClinic> VetClinincs => Set<VetClinic>();
        public DbSet<GroomerSalon> GroomerSalons => Set<GroomerSalon>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DogsCompanionContext(DbContextOptions<DogsCompanionContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Constants.SchemaName);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasData(DogsCompanionContextSeed.PredefinedUsers);
            });

            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasData(DogsCompanionContextSeed.PredefinedDogs);
            });

            modelBuilder.Entity<VetClinic>(entity =>
            {
            entity.HasData(DogsCompanionContextSeed.PredefinedVetClinics);
            entity
                .Property(b => b.OpeningHours)
                .HasConversion(
                    openingHours => JsonConvert.SerializeObject(openingHours), 
                    dbValue => JsonConvert.DeserializeObject<List<OpeningHours>>(string.IsNullOrEmpty(dbValue) ? "[]" : dbValue));
            });

            modelBuilder.Entity<GroomerSalon>(entity =>
            {
                entity.HasData(DogsCompanionContextSeed.PredefinedVetGroomerSalons);
                entity
                .Property(b => b.OpeningHours)
                .HasConversion(
                    openingHours => JsonConvert.SerializeObject(openingHours),
                    dbValue => JsonConvert.DeserializeObject<List<OpeningHours>>(string.IsNullOrEmpty(dbValue) ? "[]" : dbValue));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
