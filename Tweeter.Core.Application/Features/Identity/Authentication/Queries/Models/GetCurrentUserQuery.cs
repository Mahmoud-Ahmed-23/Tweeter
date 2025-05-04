using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Queries.Models
{
    public class GetCurrentUserQuery : IRequest<Response<ReturnUserDto>>
    {
        public GetCurrentUserQuery()
        {
        }
        public GetCurrentUserQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
