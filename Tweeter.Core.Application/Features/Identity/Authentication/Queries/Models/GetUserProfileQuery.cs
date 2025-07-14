using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Queries.Models
{
	public class GetUserProfileQuery:IRequest<Response<UserProfileToReturn>>
	{
		public SpecParams SpecParams { get; set; }
	}
}
