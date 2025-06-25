using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Commands.Models
{
    public class DeleteHashtagCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public DeleteHashtagCommand(int id)
        {
            Id = id;
        }


    }
}
