using System.Threading.Tasks;
using MTNTripPlanner.Services.Trip.Core.Events;

namespace MTNTripPlanner.Services.Trip.Application.Events
{
    public interface IDomainEventHandler<in T> where T : class, IDomainEvent
    {
        Task HandleAsync(T @event);
    }
}