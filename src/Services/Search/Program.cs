using BuildingBlocks.Messaging.MassTransit;
using Search;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

var host = builder.Build();
host.Run();
