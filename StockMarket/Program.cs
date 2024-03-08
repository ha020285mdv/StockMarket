using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockMarket.Database;
using StockMarket.Middlewares;

namespace StockMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Craete database context
            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("API-KEY", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Description = "API Key",
                    Name = "X-API-KEY",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Scheme = "bearer"
                });



                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "API-KEY"
                            }
                        },
                        new List<string>()
                    }
                });

            });

            var app = builder.Build();

            // Create and migrate database
            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
            }

            //////////// SEED + migrate ////////////


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
