using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Shared.Models;

namespace Tweeter.Infrastructure.Persistence._Data
{
    internal class TweeterDbInitializer(TweeterDbContext _dbContext, RoleManager<IdentityRole> roleManager) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
        public async Task SeedAsync()
        {
            var roles = new[] { Roles.Admin, Roles.User };

            if (!_dbContext.Roles.Any())
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));

                }
            }
        }
    }
}
