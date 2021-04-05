using System;
using Convey.CQRS.Queries;
using MTNTripPlanner.Services.Trip.Application.DTO;

namespace MTNTripPlanner.Services.Trip.Application.Queries
{
    public class GetTrip : IQuery<TripDto>
    {
        public Guid TripId { get; set; }
    }
}