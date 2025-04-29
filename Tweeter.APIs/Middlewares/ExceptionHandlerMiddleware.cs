using System.Net;
using System.Text.Json;
using Tweeter.Shared.ErrorModule.Errors;
using Tweeter.Shared.ErrorModule.Exeptions;

namespace Tweeter.APIs.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{


			try
			{

				await _next(httpContext);

				if (httpContext.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
				{
					httpContext.Response.ContentType = "application/json";
					var response = new ApiResponse((int)HttpStatusCode.MethodNotAllowed, $"Method Not Allowed");
					await httpContext.Response.WriteAsync(response.ToString());
				}

			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
				{
					_logger.LogError(ex, ex.Message);
				}

				await HandleExeptionAsync(httpContext, ex);
			}




		}

		private async Task HandleExeptionAsync(HttpContext httpContext, Exception ex)
		{
			ApiResponse response;

			switch (ex)
			{
				case NotFoundException notFound:

					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(404, notFound.Message);
					await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));

					break;

				case ValidationException validationExeption:

					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					httpContext.Response.ContentType = "application/json";
					response = new ApiValidationErrorResponse(ex.Message) { Errors = validationExeption.Errors };
					await httpContext.Response.WriteAsync(response.ToString());

					break;


				case BadRequestException badRequest:

					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(400, badRequest.Message);
					await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));

					break;

				case UnAuthorizedException unAuthorized:

					httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(401, unAuthorized.Message);
					await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));

					break;

				default:
					response = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
						: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);

					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					httpContext.Response.ContentType = "application/json";

					await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));


					break;
			}

		}
	}
}
