using NOS.Engineering.Challenge.API.Extensions;

var builder = WebApplication.CreateBuilder(args)
        .ConfigureWebHost()
        .RegisterServices();
var config = builder.Configuration;
var services = builder.Services;

builder.Services.AddControllers();

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();

if (!app.Environment.IsDevelopment())
{
    logger.LogInformation("Using production pipeline");
    app.UseExceptionHandler("/Error");
}

RedisConnection redisConnection = new();
config.GetSection("RedisConnection").Bind(redisConnection);

services.AddSingleton<IConnectionMultiplexer>(option =>
   ConnectionMultiplexer.Connect(new ConfigurationOptions
   {
       EndPoints = { $"{redisConnection.Host}:{redisConnection.Port}" },
       AbortOnConnectFail = false,
       Ssl = redisConnection.IsSSL,
       Password = redisConnection.Password

   }));

// Redis test
app.MapGet("/", async (IConnectionMultiplexer redis) =>
{
    var result = await redis.GetDatabase().PingAsync();
    try
    {
        return result.CompareTo(TimeSpan.Zero) > 0 ? $"PONG: {result}" : null;
    }
    catch (RedisTimeoutException e)
    {
        throw;
    }
});

app.MapControllers();
app.UseSwagger()
    .UseSwaggerUI();
    
app.Run();

