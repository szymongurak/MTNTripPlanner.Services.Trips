using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using MTNTripPlanner.Services.Trip.Application.DTO;
using MTNTripPlanner.Services.Trip.Application.Queries;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents;

namespace MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Queries.Handlers
{ 
    internal class GetTripHandler : IQueryHandler<GetTrip, TripDto>
    {
        private readonly IMongoDatabase _database;

        public GetTripHandler(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<TripDto> HandleAsync(GetTrip query)
        {
            var document = await _database.GetCollection<TripDocument>("trips")
                .Find(r => r.Id == query.TripId)
                .SingleOrDefaultAsync();

            return document?.AsDto();
        }
    }
}