using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Contracts.Infrastructure;

namespace Tweeter.Apis.Controllers.Filters
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _timetoliveinseconds;

		public CachedAttribute(int timetoliveinseconds)
		{
			_timetoliveinseconds = timetoliveinseconds;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var responseCachedServices = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

			var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

			var response = await responseCachedServices.GetCacheKey(cacheKey);

			if (!string.IsNullOrEmpty(response))
			{
				var result = new ContentResult()
				{
					Content = response,
					ContentType = "application/json",
					StatusCode = 200
				};

				context.Result = result;
				return;
			}

			var executedActionContext = await next.Invoke();

			if (executedActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
			{
				await responseCachedServices.SetCache(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timetoliveinseconds));
			}
		}

		private string GenerateCacheKeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();

			keyBuilder.Append(request.Path);

			foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
			{
				keyBuilder.Append($"|{key}-{value}");
			}

			return keyBuilder.ToString();
		}
	}
}
