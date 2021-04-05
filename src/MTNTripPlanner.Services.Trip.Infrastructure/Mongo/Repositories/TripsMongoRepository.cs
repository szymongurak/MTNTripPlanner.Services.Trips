using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MTNTripPlanner.Services.Trip.Core.Entities;
using MTNTripPlanner.Services.Trip.Core.Repositories;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Repositories
{
    internal sealed class TripsMongoRepository : ITripRepository
    {
        private readonly IMongoRepository<TripDocument, Guid> _repository;

        public TripsMongoRepository(IMongoRepository<TripDocument, Guid> repository)
        {
            _repository = repository;
        }
        
        public async Task<Core.Entities.Trip> GetAsync(AggregateId id)
        {
            var document = await _repository.GetAsync(r => r.Id == id);

            return document?.AsEntity();
        }

        public Task<bool> ExistsAsync(AggregateId id) 
            => _repository.ExistsAsync(r => r.Id == id);

        public Task AddAsync(Core.Entities.Trip trip) 
            => _repository.AddAsync(trip.AsDocument());

        public Task UpdateAsync(Core.Entities.Trip trip)
            => _repository.Collection.ReplaceOneAsync(r => r.Id == trip.Id && r.Version < trip.Version,
                trip.AsDocument());

        public Task DeleteAsync(AggregateId id)
            => _repository.DeleteAsync(id);
    }
}