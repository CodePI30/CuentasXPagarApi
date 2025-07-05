using CuentasXPagar.data.DbContextSqlServer;
using Microsoft.EntityFrameworkCore;

namespace CuentasXPagar.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // *** AGREGAR CORS ***
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirReactLocalhost",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000") // Puerto donde corre React
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection String 'DefaultConnection' not found");

            // Registrar el DbContext usando SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // *** USAR CORS ***
            app.UseCors("PermitirReactLocalhost");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
