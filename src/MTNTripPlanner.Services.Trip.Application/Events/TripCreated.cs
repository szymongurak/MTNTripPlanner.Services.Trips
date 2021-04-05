using System;
using Convey.CQRS.Events;

namespace MTNTripPlanner.Services.Trip.Application.Events
{
    public class TripCreated : IEvent
    {
        [Contract]
        public TripCreated(Guid tripId)
        {
            TripId = tripId;
        }

        public Guid TripId { get; }
    }
}