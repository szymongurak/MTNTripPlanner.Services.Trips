using System;
using Convey.CQRS.Events;

namespace MTNTripPlanner.Services.Trip.Application.Events.Rejected
{
    [Contract]
    public class CreateTripRejected : IRejectedEvent
    {
        public Guid TripId { get; }
        public string Reason { get; }
        public string Code { get; }

        public CreateTripRejected(Guid tripId, string reason, string code)
        {
            TripId = tripId;
            Reason = reason;
            Code = code;
        }
    }
}