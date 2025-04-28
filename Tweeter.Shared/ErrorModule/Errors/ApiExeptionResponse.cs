using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace Tweeter.Shared.ErrorModule.Errors
{
	public class ApiExeptionResponse : ApiResponse
	{
		public string? Details { get; set; }

		public ApiExeptionResponse(int statuscode, string? message = null, string? details = null)
			: base(statuscode, message)
		{
			Details = details;
		}

		public override string ToString()
		{
			return JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
		}
	}
}
