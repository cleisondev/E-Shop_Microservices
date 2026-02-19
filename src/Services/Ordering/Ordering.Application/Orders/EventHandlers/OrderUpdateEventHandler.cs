using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers
{
    internal class OrderUpdateEventHandler(ILogger<OrderUpdateEventHandler> logger)
        : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling OrderUpdatedEvent for Order Id: {OrderId}", notification.order.Id);
            //notification.order.Id.ToString();
            throw new NotImplementedException();
        }
    }
}
