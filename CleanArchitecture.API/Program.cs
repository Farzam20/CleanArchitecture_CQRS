using CleanArchitecture.API.Utilities;
using CleanArchitecture.API.Utilities.Middelwares;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);

//builder.Services.AddDefaultIdentity

var app = builder.Build();

//.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
var dbContext = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
var userManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<IdentityUser>>();
var roleManager = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
DataInitializer.Initialize(dbContext, userManager, roleManager, app.Configuration);

// Configure the HTTP request pipeline.

app.UseCustomExceptionHandler();

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
