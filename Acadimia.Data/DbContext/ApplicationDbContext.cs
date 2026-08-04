using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Acadimia.Data.Models;
//using Acadimia.Data.SeedHeper;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Principal;

namespace Acadimia.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //SeedHelper.Seed(builder);
            base.OnModelCreating(builder);
            //builder.Entity<UserType>().HasQueryFilter(x => !x.IsDeleted);
            //builder.Entity<Page>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Student>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Teacher>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Group>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Grade>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Father>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<UserType>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Page>().HasQueryFilter(x => !x.IsDeleted);

        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Constant> Constants { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageCategory> PageCategories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Father> Fathers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


    }

}
