using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace MTNTripPlanner.Services.Trip.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] events);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}