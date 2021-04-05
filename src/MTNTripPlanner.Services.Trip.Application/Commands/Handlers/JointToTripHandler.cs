using System.Threading.Tasks;
using Convey.CQRS.Commands;
using MTNTripPlanner.Services.Trip.Application.Services;
using MTNTripPlanner.Services.Trip.Core.Repositories;
using MTNTripPlanner.Services.Trip.Core.ValueObjects;

namespace MTNTripPlanner.Services.Trip.Application.Commands.Handlers
{
    public class JoinToTripHandler : ICommandHandler<JoinToTrip>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IEventProcessor _eventProcessor;

        public JoinToTripHandler(ITripRepository tripRepository, IEventProcessor eventProcessor)
        {
            _tripRepository = tripRepository;
            _eventProcessor = eventProcessor;
        }
        
        public async Task HandleAsync(JoinToTrip command)
        {
            var trip = await _tripRepository.GetAsync(command.TripId);

            var participant = new Participant(command.UserId);
            
            trip.AddParticipant(participant);
            await _tripRepository.UpdateAsync(trip);
            
            var trip2 = await _tripRepository.GetAsync(command.TripId);
            await _eventProcessor.ProcessAsync(trip.Events);
        }
    }
}