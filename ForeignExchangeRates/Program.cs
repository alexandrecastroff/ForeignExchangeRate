using Application.Gateways.Implementations;
using Application.Messaging.Producers;
using Application.Services;
using CrossInfrastructure.Gateways;
using CrossInfrastructure.Kafka;
using CrossInfrastructure.Mongo;
using CrossInfrastructure.Services;
using Data.Repository.Mongo;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // To preserve the default behavior, capture the original delegate to call later.
        var builtInFactory = options.InvalidModelStateResponseFactory;

        options.InvalidModelStateResponseFactory = context =>
        {
            var logger = context.HttpContext.RequestServices
                                .GetRequiredService<ILogger<Program>>();

            // Perform logging here.
            // ...

            // Invoke the default behavior, which produces a ValidationProblemDetails
            // response.
            // To produce a custom response, return a different implementation of 
            // IActionResult instead.
            return builtInFactory(context);
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDatabaseSettings"));

builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IExchangeRateGateway, ExchangeRateGateway>();

builder.Services.Configure<KafkaSettings>(
builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IForeignExchangeRateCreatedEventProducer, ForeignExchangeRateCreatedEventProducer>();
builder.Services.AddScoped<IForeignExchangeRatesService, ForeignExchangeRatesService>();

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
