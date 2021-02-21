using System;
using Convey.Types;
using MTNTripPlanner.Services.Trip.Core.Entities;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents
{
    internal sealed class TripDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Destination { get; set; }
        public int TimeStamp { get; set; }
        public TripDifficultyLevelEnum DifficultyLevel { get; set; }
    }
}