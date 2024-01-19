using Microsoft.EntityFrameworkCore;
using Iam.Services;
using FluentValidation.AspNetCore;
using Iam.Validators;
using FluentValidation;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Config.ConnectionString));
builder.Services.AddScoped<UserService>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UserDto>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseHttpsRedirection();

AppDbContext.CreateConnection(app, logger);

app.Run();