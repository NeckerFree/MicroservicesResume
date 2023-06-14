using Identity.Api.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddJwtBearer()
    .AddJwtBearer("LocalAuthIssuer");
builder.Services.AddAuthorization(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IdentityMSContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("IdentityMSConnection");
    options.UseSqlServer(connectionString);
});
var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
