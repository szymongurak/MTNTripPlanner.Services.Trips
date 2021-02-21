using System.Collections.Generic;
using Convey.CQRS.Events;
using MTNTripPlanner.Services.Trip.Core.Events;

namespace MTNTripPlanner.Services.Trip.Application.Services
{
    public interface IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
        public IEvent Map(IDomainEvent @event);
    }
}