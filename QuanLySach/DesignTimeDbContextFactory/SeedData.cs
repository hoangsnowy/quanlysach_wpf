using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLySach.Common.Helpers;
using QuanLySach.DAL.EF;
using QuanLySach.DAL.EF.Models;
using System;
using System.Threading.Tasks;

namespace QuanLySach.DesignTimeDbContextFactory
{
    public static class SeedData
    {
        public static async Task EnsureAdminAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<BookDbContext>();

            if (!await ctx.Set<User>().AnyAsync(u => u.Email == "dmin@example.com"))
            {
                var admin = new User
                {
                    Email = "admin@example.com",
                    PasswordHashed = SecureHelper.ComputeSha256Hash("Admin@123")
                };

                ctx.Set<User>().Add(admin);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
