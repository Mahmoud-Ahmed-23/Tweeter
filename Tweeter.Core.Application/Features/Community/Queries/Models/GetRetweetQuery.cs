using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Queries.Models
{
	public class GetRetweetQuery : IRequest<Response<RetweetToReturnDto>>
	{
		public int Id { get; set; }
		public GetRetweetQuery(int id)
		{
			Id = id;
		}
	}
}
