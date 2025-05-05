using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace dotnet9_ketnoigiaothuong.Services
{
    public static class DbInitializer
    {
        public static void SeedData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();

            if (!context.UserAccounts.Any(u => u.Role == "Admin"))
            {
                var adminUser = new UserAccount
                {
                    FullName = "Super Admin",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    Status = "Active"
                };

                context.UserAccounts.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
