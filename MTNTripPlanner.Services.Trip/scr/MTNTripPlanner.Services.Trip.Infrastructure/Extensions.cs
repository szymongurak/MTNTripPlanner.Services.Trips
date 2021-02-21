using System;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MTNTripPlanner.Services.Trip.Application;
using MTNTripPlanner.Services.Trip.Application.Services;
using MTNTripPlanner.Services.Trip.Core.Repositories;
using MTNTripPlanner.Services.Trip.Infrastructure.Exceptions;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Documents;
using MTNTripPlanner.Services.Trip.Infrastructure.Mongo.Repositories;
using MTNTripPlanner.Services.Trip.Infrastructure.Services;

namespace MTNTripPlanner.Services.Trip.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<ITripRepository, TripsMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            
            builder
                .AddQueryHandlers()    
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<TripDocument, Guid>("trips")
                .AddRabbitMq()
                .AddSwaggerDocs()
                .AddWebApiSwaggerDocs();
            
            
            return builder;
        }
        
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>()
                .UseSwaggerDocs()
                .UseRabbitMq();
            return app;
        }
    }
}