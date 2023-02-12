using CrossCuttingLayer.DAL.DB;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using Serilog;
using MassTransit;
using CrossCuttingLayer;
using Connect4Sports.Order.API.Consumers;
using CrossCuttingLayer.CommonDefinitions.Record;
using AWS.Logger.SeriLog;
using AWS.Logger;

var builder = WebApplication.CreateBuilder(args);
//var logger = new LoggerConfiguration()
//  .ReadFrom.Configuration(builder.Configuration)
//  .Enrich.FromLogContext()
//  .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);


//paymentQueue_skipped
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderPaidConsumer>();
    x.AddConsumer<PlayerRMQConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri(builder.Configuration.GetValue<string>("RabbitMQ:RabbitMqRootUri")), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("RabbitMQ:UserName"));
            h.Password(builder.Configuration.GetValue<string>("RabbitMQ:Password"));
        });
        cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
        cfg.UseMessageRetry(r => r.Immediate(5));
        cfg.UseInMemoryOutbox();
        cfg.ReceiveEndpoint(builder.Configuration.GetValue<string>("RabbitMQ:paymentQueue"), ep =>
        {
            ep.UseMessageRetry(retryConfigurator =>
            {
                retryConfigurator.Incremental(
                    3,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(30)
                );
            }
                    );

            ep.ConfigureConsumer<OrderPaidConsumer>(provider);
        });
        cfg.ReceiveEndpoint(builder.Configuration.GetValue<string>("RabbitMQ:playerOrderQueue"), ep =>
        {
            ep.UseMessageRetry(retryConfigurator =>
            {
                retryConfigurator.Incremental(
                    3,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(30)
                );
            });

            ep.ConfigureConsumer<PlayerRMQConsumer>(provider);
        });
        //dead letter queue
        cfg.ReceiveEndpoint(builder.Configuration.GetValue<string>("RabbitMQ:paymentQueue_skipped"), ep =>
        {
            ep.UseMessageRetry(retryConfigurator =>
            {
                retryConfigurator.Incremental(
                    3,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(30)
                );
            }
                    );

            ep.ConfigureConsumer<OrderPaidConsumer>(provider);
        });
    }));
});
builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});
//builder.Services.Configure<MassTransitHostOptions>(options =>
//{
//    options.WaitUntilStarted = true;
//    options.StartTimeout = TimeSpan.FromSeconds(30);
//    options.StopTimeout = TimeSpan.FromMinutes(1);
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.Configure<AppSettingsRecord>(builder.Configuration);

var dbConnectionString = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
builder.Services.AddDbContext<orderContext>(opt =>
    opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));
var Region = builder.Configuration.GetValue<string>("Region");

AWSLoggerConfig configuration = new AWSLoggerConfig("Serilog.Payment");
configuration.Region = Region;

var logger = new LoggerConfiguration()
.WriteTo.AWSSeriLog(configuration)
.CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger(x => x.SerializeAsV2 = true);

//app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API");

});
//}
app.UseStaticFiles();
app.UseRouting();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
app.UseAuthorization();

app.MapControllers();

app.Run();
