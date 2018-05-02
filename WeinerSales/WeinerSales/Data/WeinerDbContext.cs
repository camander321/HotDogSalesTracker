using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeinerSales.Models;

namespace WeinerSales.Models
{


    public class WeinerDbContext : IdentityDbContext<AppUser>
    {
        public virtual DbSet<Sale> Sale { get; set; }
    




        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(Startup.ConnectionString);
        }

        public WeinerDbContext(DbContextOptions<WeinerDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
