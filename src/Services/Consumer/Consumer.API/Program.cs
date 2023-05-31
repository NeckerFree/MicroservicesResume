using Consumer.API.Endpoints;
using Consumer.API.Repositories;
using Consumer.API.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
ServiceSettings serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? throw new InvalidOperationException("Section  'ServiceSettings' not found.");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(serviceProvider =>
{
    MongoDBSettings mongoSettings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>() ?? throw new InvalidOperationException("Section  'MongoDBSettings' not found.");
    var mongoClient = new MongoClient(mongoSettings.ConnectionString);
    return mongoClient.GetDatabase(serviceSettings.ServiceName);
});
builder.Services.AddSingleton<IClientsRepository, ClientsRepository>();
var app = builder.Build();
//BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapClientEndpoint();
app.Run();

