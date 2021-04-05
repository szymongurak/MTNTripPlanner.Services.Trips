using System.Collections.Generic;
using Convey.CQRS.Queries;
using MTNTripPlanner.Services.Trip.Application.DTO;

namespace MTNTripPlanner.Services.Trip.Application.Queries
{
    public class GetTrips : IQuery<IEnumerable<TripDto>>
    {
        public string Destination { get; set; }
    }
}