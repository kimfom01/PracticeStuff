using BusinessLogic.Services;
using BusinessLogic.Services.Implementation;
using DataAccess.DataContext;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IStackService, StackService>();
builder.Services.AddScoped<IFlashCardService, FlashCardService>();
builder.Services.AddScoped<IStudyAreaService, StudyAreaService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<Context>();
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();