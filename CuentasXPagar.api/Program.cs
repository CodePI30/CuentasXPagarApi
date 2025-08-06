using CuentasXPagar.api.BulkUpdate;
using CuentasXPagar.api.Endpoints;
using CuentasXPagar.api.HttpService;
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
                options.AddPolicy("PermitirTodos", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection String 'DefaultConnection' not found");

            // Registrar el DbContext usando SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddHttpClient();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ContabilidadHttpService>();
            builder.Services.AddScoped<BulkUpdateData>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CuentasXPagarAPI v1");
                c.RoutePrefix = "swagger"; // Esto mantiene la ruta en /swagger
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // *** USAR CORS ***
            app.UseCors("PermitirTodos");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
