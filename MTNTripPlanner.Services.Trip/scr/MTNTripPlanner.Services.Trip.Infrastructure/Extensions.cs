using System;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MTNTripPlanner.Services.Trip.Core.Repositories;
using MTNTripPlanner.Services.Trip.Infrastructure.Exceptions;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Repositories;

namespace MTNTripPlanner.Services.Trip.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<ITripRepository, TripsMongoRepository>();
            
            builder
                .AddQueryHandlers()    
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddMongo()
                .AddMongoRepository<TripDocument, Guid>("trips");
            
            return builder;
        }
        
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseConvey();
            return app;
        }
    }
}