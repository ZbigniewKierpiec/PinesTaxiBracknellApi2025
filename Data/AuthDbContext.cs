using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PinesExecutiveTravelApi.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "2dbce4a5-53ab-4e75-96ce-244e10edf825";
            var  writerRoleId = "691a347e-3fec-401d-88c1-efb2beef2016";


            // Create Reader and Writer Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    
                    Name="Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },
                 new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                },
            };


            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin User
            var adminUserId = "74938a76-f26a-4fc5-bf0e-d0a99d84ac18";
            var admin = new IdentityUser()
            { 
              Id = adminUserId,

              UserName = "admin@pinebraxknelltaxi.com",
              Email = "admin@pinebraxknelltaxi.com",
              NormalizedEmail = "admin@pinebraxknelltaxi.com".ToUpper(),
              NormalizedUserName= "admin@pinebraxknelltaxi.com".ToUpper()

            };

             admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
        
            builder.Entity<IdentityUser>().HasData(admin);
            // Give Roles to Admin


            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                     UserId=adminUserId,
                      RoleId=readerRoleId,
                },
                new()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId,
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
