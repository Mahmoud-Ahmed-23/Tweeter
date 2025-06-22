using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Queries.Models
{
    public class GetUnreadableNotificationsQuery : IRequest<Response<Pagination<NotificationDto>>>
    {

        public SpecParams SpecParams { get; set; }
        public GetUnreadableNotificationsQuery(SpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
