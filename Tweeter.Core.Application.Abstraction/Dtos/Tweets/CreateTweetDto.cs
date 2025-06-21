using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Tweets
{
	public class CreateTweetDto
	{
		public required string UserId { get; set; }

		public string? Content { get; set; }

		public IFormFile? ImageUrl { get; set; }
	}
}
