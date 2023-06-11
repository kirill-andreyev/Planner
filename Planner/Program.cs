using BusinessLogic.Mapping.Profiles;
using BusinessLogic.Services;
using DataAccess.Repository;
using Planner.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(EventProfile));
builder.Services.AddSwagger();

builder.Services.AddMigrationsDll(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.ConfigureRepository();
builder.Services.AddBusinessLogic();
builder.Services.AddJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
