using System.Collections.Generic;
using System.Threading.Tasks;
using MTNTripPlanner.Services.Trip.Core.Events;

namespace MTNTripPlanner.Services.Trip.Application.Services
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}