using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SitRep.Core;
using SitRep.DAL;
using SitRep.Models;
using Swashbuckle.AspNetCore.Filters;

const string AllowedDevelopmentOrigin = "_allowedDevelopmentOrigin";
const string AllowedProductionOrigin = "_allowedProductionOrigin";
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\"}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddDbContext<ISitRepContext,SitRepContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MSSQL");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedDevelopmentOrigin,
        policy => { policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedProductionOrigin,
        policy =>
        {
            policy.WithOrigins("https://zealous-flower-0589cae03.1.azurestaticapps.net").AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddTransient<SitRepSeed>();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);


var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var initializer = services.GetRequiredService<SitRepSeed>();
initializer.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseCors(AllowedDevelopmentOrigin);
}
else
{
    app.UseCors(AllowedProductionOrigin);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }