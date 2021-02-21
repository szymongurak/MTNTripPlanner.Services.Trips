using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MTNTripPlanner.Services.Trip.Application.DTO;
using MTNTripPlanner.Services.Trip.Application.Queries;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Queries.Handlers
{
    public class GetTripsHandler : IQueryHandler<GetTrips, IEnumerable<TripDto>>
    {
        private readonly IMongoDatabase _database;

        public GetTripsHandler(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<IEnumerable<TripDto>> HandleAsync(GetTrips query)
        {
            var collection = _database.GetCollection<TripDocument>("trips");

            if (string.IsNullOrEmpty(query.Destination))
            {
                var allDocuments = await collection.Find(_ => true).ToListAsync();

                return allDocuments.Select(d => d.AsDto());
            }

            var documents = collection.AsQueryable();
            documents = documents.Where(d => query.Destination == d.Destination);

            var trips = await documents.ToListAsync();

            return trips.Select(d => d.AsDto());
        }
    }
}