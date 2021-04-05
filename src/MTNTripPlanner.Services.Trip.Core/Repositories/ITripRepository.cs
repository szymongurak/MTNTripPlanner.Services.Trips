using System.Threading.Tasks;
using MTNTripPlanner.Services.Trip.Core.Entities;

namespace MTNTripPlanner.Services.Trip.Core.Repositories
{
    public interface ITripRepository
    {
        Task<Entities.Trip> GetAsync(AggregateId id);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(Entities.Trip trip);
        Task UpdateAsync(Entities.Trip trip);
        Task DeleteAsync(AggregateId id);
    }
}