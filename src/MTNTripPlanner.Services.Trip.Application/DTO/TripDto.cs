using System;
using System.Collections.Generic;
using MTNTripPlanner.Services.Trip.Core.Entities;

namespace MTNTripPlanner.Services.Trip.Application.DTO
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public TripDifficultyLevelEnum DifficultyLevel { get; set; }
        public IEnumerable<ParticipantDto> Participants { get; set; }
    }
}