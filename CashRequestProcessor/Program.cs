using CashRequestProcessor.Consumers;
using CashRequestProcessor.Counsumers;
using CashRequestProcessor.Interfaces;
using CashRequestProcessor.Repositories;
using MassTransit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//Repository
builder.Services.AddScoped<IRequestRepository, RequestRepository>();

//MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateRequestCommandConsumer>();
    x.AddConsumer<GetRequestStatusByIdQueryConsumer>();
    x.AddConsumer<GetRequestStatusByClientIdAndDepAddressQueryConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq");
        //config.ReceiveEndpoint("CreateRequestCommand", options =>
        //{
        //    //TODO: Add retry policy
        //    options.BindQueue = true;
        //    options.UseMessageRetry(retryConfig =>
        //    {
        //        retryConfig.Interval(3, TimeSpan.FromSeconds(5)); 
        //        retryConfig.Handle<Exception>();
        //    });
        //    options.ConfigureConsumer<CreateRequestCommandConsumer>(context);
        //});
        config.ConfigureEndpoints(context);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
