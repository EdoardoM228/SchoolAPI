using System;
using APISolution.Models;
using Microsoft.EntityFrameworkCore;

namespace APISolution.Data
{
    public class ClassmateContext : DbContext
    {
        public DbSet<Classmate> Classmates { get; set; }

        public DbSet<User> Users { get; set; }

        public ClassmateContext(DbContextOptions<ClassmateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("User");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.UserName).HasMaxLength(30).IsUnicode(false);
                entity.Property(e => e.UserPassword).HasMaxLength(20).IsUnicode(false);
            });
        }
    }
}

