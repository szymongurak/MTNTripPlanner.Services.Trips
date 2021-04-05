using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using MTNTripPlanner.Services.Trip.Application.Commands;
using MTNTripPlanner.Services.Trip.Application.Events;
using MTNTripPlanner.Services.Trip.Application.Services;
using MTNTripPlanner.Services.Trip.Core.Events;
using TripCreated = MTNTripPlanner.Services.Trip.Core.Events.TripCreated;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
            => @event switch
            {
                TripCreated e => new Application.Events.TripCreated(e.Trip.Id),
                ParticipantAdded e => new JoinedToTrip(e.Trip.Id, e.Participant.UserId),
                _ => null
            };
    }
}