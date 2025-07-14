using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Contracts.Infrastructure;

namespace Tweeter.Infrastructure.CacheServices
{
	internal class CacheService : ICacheService
	{
		private readonly IDatabase _database;

		public CacheService(IConnectionMultiplexer connectionMultiplexer)
		{
			_database = connectionMultiplexer.GetDatabase();
		}

		public async Task<string> GetCacheKey(string key)
		{
			var cachedValue = await _database.StringGetAsync(key);

			if (cachedValue.IsNullOrEmpty)
				return null;

			return cachedValue.ToString();
		}

		public async Task SetCache(string key, object response, TimeSpan timeToLive)
		{
			if (response == null)
				return;

			var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

			await _database.StringSetAsync(key, serializedResponse, timeToLive);
		}
	}
}
