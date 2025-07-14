using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Domain.Contracts.Infrastructure
{
	public interface ICacheService
	{
		Task<string> GetCacheKey(string key);
		Task SetCache(string key, object response, TimeSpan timeToLive);

	}
}
