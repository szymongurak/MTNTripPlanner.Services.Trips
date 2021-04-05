using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MTNTripPlanner.Services.Trip.Application.Events;
using MTNTripPlanner.Services.Trip.Application.Services;
using MTNTripPlanner.Services.Trip.Core.Events;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Services
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<IEventProcessor> _logger;

        public EventProcessor(IMessageBroker messageBroker, IEventMapper eventMapper, 
            IServiceScopeFactory serviceScopeFactory, ILogger<IEventProcessor> logger)
        {
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        
        public  async Task ProcessAsync(IEnumerable<IDomainEvent> events)
        {
            if (events is null)
            {
                return;
            }

            var integrationEvents = await HandleDomainEvents(events);
            if (!integrationEvents.Any())
            {
                return;
            }

            await _messageBroker.PublishAsync(integrationEvents);
        }

        private async Task<List<IEvent>> HandleDomainEvents(IEnumerable<IDomainEvent> events)
        {
            var integrationEvents = new List<IEvent>();
            using var scope = _serviceScopeFactory.CreateScope();
            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                _logger.LogTrace($"Handling domain event: {eventType.Name}");
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
                dynamic handlers = scope.ServiceProvider.GetServices(handlerType);
                foreach (var handler in handlers)
                {
                    await handler.HandleAsync((dynamic) @event);
                }

                var integrationEvent = _eventMapper.Map(@event);
                if (integrationEvent is null)
                {
                    continue;
                }

                integrationEvents.Add(integrationEvent);
            }

            return integrationEvents;
        }
        
        // private async Task<List<IEvent>> HandleDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents)
        // {
        //     var integrationEvents = new List<IEvent>();
        //     using var scope = _serviceScopeFactory.CreateScope();
        //     foreach (var domainEvent in domainEvents)
        //     {
        //         var domainEventType = domainEvent.GetType();
        //         _logger.LogTrace($"Handling a domain event: {domainEventType.Name}");
        //         var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEventType);
        //         dynamic handlers = scope.ServiceProvider.GetService(handlerType);
        //         foreach (var handler in handlers)
        //         {
        //             await handler.HandleAsync((dynamic) domainEvent);
        //         }
        //
        //         var integrationEvent = _eventMapper.Map(domainEvent);
        //         if (integrationEvent is null)
        //         {
        //             continue;
        //         }
        //         
        //         integrationEvents.Add(integrationEvent);
        //     }
        //
        //     return integrationEvents;
        // }
    }
}