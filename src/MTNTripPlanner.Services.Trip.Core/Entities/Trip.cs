using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MTNTripPlanner.Services.Trip.Core.Events;
using MTNTripPlanner.Services.Trip.Core.ValueObjects;

namespace MTNTripPlanner.Services.Trip.Core.Entities
{
    public class Trip : AggregateRoot
    {
        private ISet<Participant> _participants = new HashSet<Participant>();

        public IEnumerable<Participant> Participants
        {
            get => _participants;
            private set => _participants = new HashSet<Participant>(value);
        }
        
        public string Destination { get; }
        public DateTime Date { get; }
        public TripDifficultyLevelEnum DifficultyLevel { get; }

        public Trip(AggregateId id, string destination, DateTime date, TripDifficultyLevelEnum difficultyLevel, 
            IEnumerable<Participant> participants = null, int version = 0)
        {
            Id = id;
            Destination = destination;
            Date = date;
            DifficultyLevel = difficultyLevel;
            Participants = participants ?? Enumerable.Empty<Participant>();
            Version = version;
        }

        public static Trip Create(AggregateId id, string destination, DateTime date,
            TripDifficultyLevelEnum difficultyLevel, IEnumerable<Participant> participants = null)
        {
            var trip = new Trip(id, destination, date, difficultyLevel, participants);
            trip.AddEvent(new TripCreated(trip));
            return trip;
        }

        public void AddParticipant(Participant participant)
        {
            if (_participants.Add(participant))
            {
                AddEvent(new ParticipantAdded(this, participant));
            }
        }
    }
}