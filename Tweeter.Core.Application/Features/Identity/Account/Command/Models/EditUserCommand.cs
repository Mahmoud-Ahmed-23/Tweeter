using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Models
{
	public class EditUserCommand : IRequest<Response<ReturnUserDto>>
	{
		public EditUserDto EditUserDto { get; set; }
	}
}
