using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Models
{
    public class ChangePasswordCommand : IRequest<Response<ChangePasswordToReturn>>
    {

        public ChangePasswordDto ChangePasswordDto { get; set; }
    }
}
