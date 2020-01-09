using Cinema.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class CineDbContext: DbContext
    {
        public CineDbContext(DbContextOptions<CineDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorEntity>().ToTable("Actors");
            modelBuilder.Entity<ActorEntity>().HasMany(a => a.Movies).WithOne(b => b.Actor);
            modelBuilder.Entity<ActorEntity>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<MovieEntity>().ToTable("Movies");
            modelBuilder.Entity<MovieEntity>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MovieEntity>().HasOne(b => b.Actor).WithMany(a => a.Movies);

            modelBuilder.Entity<WinnerEntity>().ToTable("Winners");
            modelBuilder.Entity<WinnerEntity>().Property(b => b.Id).ValueGeneratedOnAdd();

        }

        public DbSet<ActorEntity> Actors { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<WinnerEntity> Winners { get; set; }
    }
}
