using System;

namespace MTNTripPlanner.Services.Trip.Core.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public override string Code => "invalid_aggregate_id";
        public Guid Id { get; }

        public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
        {
            Id = id;
        }
    }
}