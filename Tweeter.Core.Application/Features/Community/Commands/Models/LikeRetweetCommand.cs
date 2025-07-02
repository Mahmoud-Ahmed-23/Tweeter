using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Commands.Models
{
	public class LikeRetweetCommand : IRequest<Response<string>>
	{
		public int RetweetId { get; set; }
		public LikeRetweetCommand(int retweetId)
		{
			RetweetId = retweetId;
		}
	}
}
