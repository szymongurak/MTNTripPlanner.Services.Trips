using System;
using Convey.MessageBrokers.RabbitMQ;
using MTNTripPlanner.Services.Trip.Application.Events.Rejected;
using MTNTripPlanner.Services.Trip.Application.Exceptions;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                TripAlreadyExistsException ex => new CreateTripRejected(ex.TripId, ex.Message, ex.Code),
                _ => null
            };
    }
}