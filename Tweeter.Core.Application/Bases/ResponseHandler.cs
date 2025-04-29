using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Bases
{
	public class ResponseHandler
	{

		public Response<T> Success<T>(T entity, string message = null!, object Meta = null!)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = HttpStatusCode.OK,
				Succeeded = true,
				Message = message == null ? "Success" : message,
				Meta = Meta,
				ErrorType = ErrorType.None.ToString()
			};
		}

		public Response<T> Fail<T>(string message, ErrorType errorType = ErrorType.Unexpected)
		{

			HttpStatusCode statusCode;

			switch (errorType)
			{
				case ErrorType.NotFound:
					statusCode = HttpStatusCode.NotFound;
					break;
				case ErrorType.Unauthorized:
					statusCode = HttpStatusCode.Unauthorized;
					break;
				case ErrorType.Validation:
					statusCode = HttpStatusCode.BadRequest;
					break;
				default:
					statusCode = HttpStatusCode.BadRequest;
					break;
			}

			return new Response<T>
			{
				Succeeded = false,
				Message = message,
				ErrorType = errorType.ToString(),
				StatusCode = statusCode,
			};
		}
	}

}
