using System;
using Convey.CQRS.Events;

namespace MTNTripPlanner.Services.Trip.Application.Events
{
    [Contract]
    public class JoinedToTrip : IEvent
    {
        public Guid TripId { get; }
        public Guid UserId { get; }

        public JoinedToTrip(Guid tripId, Guid userId)
            => (TripId, UserId) = (tripId, userId);
    }
}