using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Commands.Models
{
	public class UpdateRetweetCommand : IRequest<Response<RetweetToReturnDto>>
	{
		public int RetweetId { get; set; }
		public string Content { get; set; }
		public UpdateRetweetCommand(int retweetId, string content)
		{
			RetweetId = retweetId;
			Content = content;
		}
	}
}
