using System;
using System.Linq;
using MTNTripPlanner.Services.Trip.Application.DTO;
using MTNTripPlanner.Services.Trip.Core.ValueObjects;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents
{
    internal static class Extenstions
    {
        public static Core.Entities.Trip AsEntity(this TripDocument document)
            => new Core.Entities.Trip(document.Id, document.Destination, document.TimeStamp.AsDateTime(),
                document.DifficultyLevel, document.Participants?.Select(p => new Participant(p.UserId)), document.Version);

        public static TripDocument AsDocument(this Core.Entities.Trip trip)
            => new TripDocument
            {
                Id = trip.Id,
                Version = trip.Version,
                Destination = trip.Destination,
                TimeStamp = trip.Date.AsDaysSinceEpoch(),
                DifficultyLevel = trip.DifficultyLevel,
                Participants = trip.Participants?.Select(p => new ParticipantDocument{UserId = p.UserId})
            };
        
        public static TripDto AsDto(this TripDocument document)
            => new TripDto
            {
                Id = document.Id,
                Destination = document.Destination,
                Date = document.TimeStamp.AsDateTime(),
                DifficultyLevel = document.DifficultyLevel,
                Participants = document.Participants?.Select(p => new ParticipantDto{UserId = p.UserId})
            };
        
        internal static int AsDaysSinceEpoch(this DateTime dateTime)
            => (dateTime - new DateTime()).Days;
        
        internal static DateTime AsDateTime(this int daysSinceEpoch)
        => new DateTime().AddDays(daysSinceEpoch);
    }
}