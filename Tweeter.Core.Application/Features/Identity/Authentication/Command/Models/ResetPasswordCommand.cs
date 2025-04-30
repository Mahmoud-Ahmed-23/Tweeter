using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Models
{
    public class ResetPasswordCommand : IRequest<Response<ReturnUserDto>>
    {

        public ResetPasswordByEmailDto ResetPasswordByEmailDto { get; set; }
    }
}
