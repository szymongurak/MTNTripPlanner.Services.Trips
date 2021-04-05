using System;

namespace MTNTripPlanner.Services.Trip.Application.Exceptions
{
    public class TripAlreadyExistsException : AppException
    {
        public override string Code => "trip_already_exists";
        public Guid TripId { get; }
        

        public TripAlreadyExistsException(Guid tripId) : base($"Trip with id: {tripId} already exists")
        {
            TripId = tripId;
        }
    }
}