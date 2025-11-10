using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Persistance.Context.Identity
{
    public class AppIdentityAppDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole,string>
    {
        public AppIdentityAppDbContext(DbContextOptions<AppIdentityAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppIdentityUser>(b =>
            {
                b.ToTable("Users");
            });
            builder.Entity<AppIdentityRole>(b =>
            {
                b.ToTable("Roles");
            });

        }
    }
}
