using System.Threading.Tasks;
using Convey.CQRS.Commands;
using MTNTripPlanner.Services.Trip.Application.Exceptions;
using MTNTripPlanner.Services.Trip.Application.Services;
using MTNTripPlanner.Services.Trip.Core.Repositories;

namespace MTNTripPlanner.Services.Trip.Application.Commands.Handlers
{
    public class AddTripHandler : ICommandHandler<AddTrip>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IEventProcessor _eventProcessor;

        public AddTripHandler(ITripRepository tripRepository, IEventProcessor eventProcessor)
        {
            _tripRepository = tripRepository;
            _eventProcessor = eventProcessor;
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
            await _eventProcessor.ProcessAsync(trip.Events);
        }
    }
}