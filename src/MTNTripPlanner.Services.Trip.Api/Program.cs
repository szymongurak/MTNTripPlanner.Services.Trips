using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MTNTripPlanner.Services.Trip.Application;
using MTNTripPlanner.Services.Trip.Application.Commands;
using MTNTripPlanner.Services.Trip.Application.DTO;
using MTNTripPlanner.Services.Trip.Application.Queries;
using MTNTripPlanner.Services.Trip.Infrastructure;

namespace MTNTripPlanner.Services.TripApi
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args).Build().RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    // .UseRouting()
                    // .UseEndpoints(e => e.MapControllers())
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(
                                ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetTrips, IEnumerable<TripDto>>("trips")
                        .Get<GetTrip, TripDto>("trips/{tripId}")
                        .Post<AddTrip>("trips", afterDispatch:(cmd, ctx) => ctx.Response.Created($"trips/{cmd.TripId}"))
                        .Post<JoinToTrip>("participant", afterDispatch:(cmd, ctx) => ctx.Response.Created($"participants/{cmd.UserId}"))
                    )
                );
    }
}

//    "iisExpress": {
//      "applicationUrl": "http://localhost:29710",
//      "sslPort": 44344
//    }