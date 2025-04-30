using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Models
{
    public class LougOutCommand : IRequest<Response<SuccessDto>>
    {
    }
}
