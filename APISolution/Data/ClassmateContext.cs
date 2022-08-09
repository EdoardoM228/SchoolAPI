using System;
using APISolution.Models;
using Microsoft.EntityFrameworkCore;

namespace APISolution.Data
{
    public class ClassmateContext : DbContext
    {
        public DbSet<Classmate> Classmates { get; set; }

        public ClassmateContext(DbContextOptions<ClassmateContext> options) : base(options)
        {
        }
    }
}

