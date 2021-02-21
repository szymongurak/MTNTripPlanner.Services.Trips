using System;
using Convey.CQRS.Commands;
using MTNTripPlanner.Services.Trip.Core.Entities;

namespace MTNTripPlanner.Services.Trip.Application.Commands
{
    public class AddTrip : ICommand
    {
        public Guid TripId { get; }
        public string Destination { get; }
        public DateTime Date { get; }
        public TripDifficultyLevelEnum DifficultyLevel { get; }
        
        public AddTrip(Guid tripId, string destination, DateTime date, TripDifficultyLevelEnum difficultyLevel)
        {
            TripId = tripId;
            Destination = destination;
            Date = date;
            DifficultyLevel = difficultyLevel;
        }
    }
}