namespace MTNTripPlanner.Services.Trip.Core.Events
{
    public class TripCreated : IDomainEvent
    {
        public Entities.Trip Trip { get; }

        public TripCreated(Entities.Trip trip) => Trip = trip;
    }
}