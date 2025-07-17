using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Models
{
	public class ConfirmUserEmailCommand : IRequest<Response<string>>
	{
		public string Email { get; set; }
		public int Code { get; set; }
		public ConfirmUserEmailCommand(string email, int code)
		{
			Email = email;
			Code = code;
		}
	}
}
