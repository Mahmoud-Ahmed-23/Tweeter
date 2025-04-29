using Tweeter.Core.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace Tweeter.Apis.Controllers.Controllers.Base
{
	[ApiController]
	public class BaseApiController : ControllerBase
	{
		private IMediator _mediator;
		protected IMediator mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

		public ObjectResult NewResult<T>(Response<T> response)
		{
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					return new OkObjectResult(response);
				case HttpStatusCode.Created:
					return new CreatedResult(string.Empty, response);
				case HttpStatusCode.Unauthorized:
					return new UnauthorizedObjectResult(response);
				case HttpStatusCode.BadRequest:
					return new BadRequestObjectResult(response);
				case HttpStatusCode.NotFound:
					return new NotFoundObjectResult(response);
				case HttpStatusCode.Accepted:
					return new AcceptedResult(string.Empty, response);
				default:
					return new BadRequestObjectResult(response);
			}

		}
	}
}
