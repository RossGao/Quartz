using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Models;

namespace WebAppTest.Repositories
{
    public class DogDBcontext : DbContext
    {
        public DogDBcontext(DbContextOptions<DogDBcontext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dog> Dogs { get; set; }
    }
}
