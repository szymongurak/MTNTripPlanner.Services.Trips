using System;
using Convey.CQRS.Commands;

namespace MTNTripPlanner.Services.Trip.Application.Commands
{
    [Contract]
    public class JoinToTrip : ICommand
    {
        public Guid TripId { get; }
        public Guid UserId { get; }

        public JoinToTrip(Guid tripId, Guid userId)
            => (TripId, UserId) = (tripId, userId);
    }
}