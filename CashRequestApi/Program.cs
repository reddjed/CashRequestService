using CashRequestApi.Core.Requests.Commands.CreateRequest;
using CashRequestApi.Options;
using CashRequestApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//builder.Services.AddSingleton<ConnectionFactory>(provider =>
//{
//    var factory = new ConnectionFactory()
//    {

//        HostName = "rabbitmq",
//        UserName = "guest",
//        Password = "guest",
//    };
//    return factory;
//});

builder.Services.AddScoped<RabbitMQService>();

builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<CreateRequestCommand>();

    x.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq");
    });
}); 

//mediator
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.Configure<RabbitConfig>(builder.Configuration.GetSection(RabbitConfig.Section));
builder.Services.Configure<RabbitQueuesConfig>(builder.Configuration.GetSection(RabbitQueuesConfig.Section));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
