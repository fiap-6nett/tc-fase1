using Microsoft.AspNetCore.Mvc;
using uBee.Api.Extensions;
using uBee.Application;
using uBee.Infrastructure;
using uBee.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.EnsureDatabaseCreated();

app.ConfigureSwagger();

app.Run();
