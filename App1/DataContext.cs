using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class DataContext : DbContext
    {
        public DbSet<Film> FilmSet { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=app;Username=postgres;Password=1111;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>().ToTable("Film");

            modelBuilder.Entity<Film>().HasKey(c => c.Id);

            modelBuilder.Entity<Film>().Property(c => c.Id).HasColumnName("Id");
            modelBuilder.Entity<Film>().Property(c => c.Title).HasColumnName("Title");
            modelBuilder.Entity<Film>().Property(c => c.Director).HasColumnName("Director");
            modelBuilder.Entity<Film>().Property(c => c.Writer).HasColumnName("Writer");
            modelBuilder.Entity<Film>().Property(c => c.Genre).HasColumnName("Genre");
            modelBuilder.Entity<Film>().Property(c => c.Year).HasColumnName("Year");
        }

    }
}
