using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Shared.ErrorModule.Errors;

namespace Tweeter.Apis.Controllers.Controllers.Common
{
	[Route("Errors/{Code}")]
	[ApiExplorerSettings(IgnoreApi = false)]
	public class ErrorController : BaseApiController
	{
		[HttpGet]
		public IActionResult Error(int Code)
		{
			if (Code == (int)HttpStatusCode.NotFound)
			{
				var respnse = new ApiResponse((int)HttpStatusCode.NotFound, $"the requested endpoint  is not found");
				return NotFound(respnse);
			}
			else if (Code == (int)HttpStatusCode.Forbidden)
			{
				var respnse = new ApiResponse((int)HttpStatusCode.Unauthorized, $"You Are Not Authorized");
				return StatusCode((int)HttpStatusCode.Unauthorized, respnse);

			}
			else if (Code == (int)HttpStatusCode.MethodNotAllowed)
			{
				var respnse = new ApiResponse((int)HttpStatusCode.MethodNotAllowed, $"Method Not Allowed");
				return StatusCode(Code, respnse);

			}
			return StatusCode(Code, new ApiResponse(Code));

		}
	}
}
