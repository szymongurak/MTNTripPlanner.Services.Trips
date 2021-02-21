using System;
using MTNTripPlanner.Services.Trip.Application.DTO;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents
{
    internal static class Extenstions
    {
        public static Core.Entities.Trip AsEntity(this TripDocument document)
            => new Core.Entities.Trip(document.Id, document.Destination, document.TimeStamp.AsDateTime(),
                document.DifficultyLevel, document.Version);

        public static TripDocument AsDocument(this Core.Entities.Trip trip)
            => new TripDocument
            {
                Id = trip.Id,
                Version = trip.Version,
                Destination = trip.Destination,
                TimeStamp = trip.Date.AsDaysSinceEpoch(),
                DifficultyLevel = trip.DifficultyLevel
            };
        
        public static TripDto AsDto(this TripDocument document)
            => new TripDto
            {
                Id = document.Id,
                Destination = document.Destination,
                Date = document.TimeStamp.AsDateTime(),
                DifficultyLevel = document.DifficultyLevel
            };
        
        internal static int AsDaysSinceEpoch(this DateTime dateTime)
            => (dateTime - new DateTime()).Days;
        
        internal static DateTime AsDateTime(this int daysSinceEpoch)
        => new DateTime().AddDays(daysSinceEpoch);
    }
}