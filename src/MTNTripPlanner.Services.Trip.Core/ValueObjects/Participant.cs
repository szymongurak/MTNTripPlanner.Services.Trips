using System;

namespace MTNTripPlanner.Services.Trip.Core.ValueObjects
{
    public struct Participant
    {
        public Participant(Guid userId)
            => UserId = userId;

        public Guid UserId { get; }
    }
}