using System.Security.Claims;
using Tweeter.Core.Application.Abstraction;

namespace Tweeter.APIs.Services
{
	public class LoggedInUserService : ILoggedInUserService
	{
		private readonly IHttpContextAccessor? _httpcontextAccessor;
		public string? UserId { get; set; }

		public LoggedInUserService(IHttpContextAccessor? httpcontextAccessor)
		{
			_httpcontextAccessor = httpcontextAccessor;

			UserId = _httpcontextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
		}

	}
}
