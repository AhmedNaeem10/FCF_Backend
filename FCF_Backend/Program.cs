using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FCF.Data;
using FCF.Config;
using FCF.Helpers;
using FCF.Services.Interfaces;
using FluentValidation.AspNetCore;
using FCF.Services.Services;
using FCF.Models.Validators.ModelValidators.User;
using FCF.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<MainDBContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("Conn") ?? throw new InvalidOperationException("Connection string 'MainDbContext' not found."), b => b.MigrationsAssembly("FCF.Data")));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddTransient<MainDBContext>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
