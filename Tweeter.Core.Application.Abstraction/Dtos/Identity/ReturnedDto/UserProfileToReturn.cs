using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto
{
	public class UserProfileToReturn
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string? ProfilePictureUrl { get; set; }
		public DateTime CreatedAt { get; set; }

		public int FollowersCount { get; set; }
		public int FollowingCount { get; set; }


		public List<RetweetToReturnDto>? Posts { get; set; }
		

	}
}
