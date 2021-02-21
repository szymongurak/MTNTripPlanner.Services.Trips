using System.Threading.Tasks;
using Convey.CQRS.Commands;
using MTNTripPlanner.Services.Trip.Application.Events;
using MTNTripPlanner.Services.Trip.Application.Exceptions;
using MTNTripPlanner.Services.Trip.Application.Services;
using MTNTripPlanner.Services.Trip.Core.Repositories;

namespace MTNTripPlanner.Services.Trip.Application.Commands.Handlers
{
    public class AddTripHandler : ICommandHandler<AddTrip>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public AddTripHandler(ITripRepository tripRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
        {
            _tripRepository = tripRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }
        
        public async Task HandleAsync(AddTrip command)
        {
            if (await _tripRepository.ExistsAsync(command.TripId))
            {
                throw new TripAlreadyExistsException(command.TripId);
            }

            var trip = Core.Entities.Trip.Create(command.TripId, command.Destination, command.Date,
                command.DifficultyLevel);
            await _tripRepository.AddAsync(trip);
            var events = _eventMapper.MapAll(trip.Events);
            await _messageBroker.PublishAsync(events);
        }
    }
}