using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Models
{
    public class ForgetPasswordCommand : IRequest<Response<SuccessDto>>
    {
        public ForgetPasswordByEmailDto ForgetPasswordByEmailDto { get; set; }

    }
}
