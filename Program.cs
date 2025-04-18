
using IMS_InventoryManagmentSystem_.Data;
using IMS_InventoryManagmentSystem_.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IMS_InventoryManagmentSystem_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .WriteTo.Console()
             .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
             .Enrich.FromLogContext()
             .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddControllers();
            // Learn more about configuring Swaggser/OgpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
