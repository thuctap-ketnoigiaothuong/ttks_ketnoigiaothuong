using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace dotnet9_ketnoigiaothuong.Services
{
    public class DbInitializer : BaseRepository
    {
        public DbInitializer(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public void SeedData()
        {
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
