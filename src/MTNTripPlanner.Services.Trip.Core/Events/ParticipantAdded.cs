using MTNTripPlanner.Services.Trip.Core.ValueObjects;

namespace MTNTripPlanner.Services.Trip.Core.Events
{
    public class ParticipantAdded : IDomainEvent
    {
        public Entities.Trip Trip { get; }
        public Participant Participant { get; }

        public ParticipantAdded(Entities.Trip trip, Participant participant)
            => (Trip, Participant) = (trip, participant);
    }
}