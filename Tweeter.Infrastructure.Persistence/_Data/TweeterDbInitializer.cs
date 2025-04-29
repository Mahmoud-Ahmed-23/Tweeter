using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Contracts.Persistence;

namespace Tweeter.Infrastructure.Persistence._Data
{
	internal class TweeterDbInitializer(TweeterDbContext _dbContext) : IDbInitializer
	{
		public async Task InitializeAsync()
		{
			var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
			
			if (pendingMigrations.Any())
			{
				await _dbContext.Database.MigrateAsync();
			}
		}
	}
}
