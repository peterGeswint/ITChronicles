using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IT_Chronicles.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "f376f12e-f050-4966-acfa-c0042727714e";
            var superAdminRoleId = "83a26f2d-4b69-45e1-9b9c-4fb57ae4f67a";
            var adminRoleId = "08356c8b-fe2a-4d0d-b385-b76579f2ae20";

            // Seed roles (User, Admin, SuperAdmin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdmin user
            var superAdminId = "38272fd9-900a-4c5e-951c-1755cd7a7fca";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadminuser@itchronicles.com",
                Email = "superadminuser@itchronicles.com",
                NormalizedEmail = "SUPERADMINUSER@ITCHRONICLES.COM",
                NormalizedUserName = "SUPERADMINUSER@ITCHRONICLES.COM",
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Super@123!");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Seed SuperAdmin user roles
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
