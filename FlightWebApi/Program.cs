
using System.Text;
using FlightLibrary.Models;
using FlightLibrary.Repos;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

namespace FlightWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            FlightDBContext context = new FlightDBContext();
            context.Database.EnsureCreated();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddScoped<IFlightRepository, EFFlightRepository>();
            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authentication using Bearer scheme"
                });
                options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement {
        { new OpenApiSecuritySchemeReference("Bearer", doc), new List<string>() }
    });
            });
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://www.snrao.com",
                    ValidAudience = "https://www.snrao.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("I am Bond, James Bond. I am the best spy in the world. I am invincible."))
                };
            });

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
