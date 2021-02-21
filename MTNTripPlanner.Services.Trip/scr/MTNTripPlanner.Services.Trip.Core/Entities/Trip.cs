using System;
using System.Collections;
using System.Collections.Generic;
using MTNTripPlanner.Services.Trip.Core.Events;

namespace MTNTripPlanner.Services.Trip.Core.Entities
{
    public class Trip : AggregateRoot
    {
        public string Destination { get; }
        public DateTime Date { get; }
        public TripDifficultyLevelEnum DifficultyLevel { get; }

        public Trip(AggregateId id, string destination, DateTime date, TripDifficultyLevelEnum difficultyLevel, int version = 0)
        {
            Id = id;
            Destination = destination;
            Date = date;
            DifficultyLevel = difficultyLevel;
            Version = version;
        }

        public static Trip Create(AggregateId id, string destination, DateTime date,
            TripDifficultyLevelEnum difficultyLevel)
        {
            var trip = new Trip(id, destination, date, difficultyLevel);
            trip.AddEvent(new TripCreated(trip));
            return trip;
        }
    }
}