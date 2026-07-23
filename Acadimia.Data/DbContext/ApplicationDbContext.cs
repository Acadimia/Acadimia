using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Acadimia.Data.Models;
//using Acadimia.Data.SeedHeper;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Principal;

namespace Acadimia.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //SeedHelper.Seed(builder);
            base.OnModelCreating(builder);
           // builder.Entity<UserType>().HasQueryFilter(x => !x.IsDeleted);
            //builder.Entity<Page>().HasQueryFilter(x => !x.IsDeleted);
           
        }
         

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserType> UserTypes { get; set; }
        //public DbSet<Constant> Constants { get; set; }
        //public DbSet<Module> Modules { get; set; }
   
       

    }
    
}
