
using IMS_InventoryManagmentSystem_.Data;
using IMS_InventoryManagmentSystem_.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IMS_InventoryManagmentSystem_.Service.IService;
using IMS_InventoryManagmentSystem_.Service;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using IMS_InventoryManagmentSystem_.Repositories;

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
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepo, ProductRepository>();
            builder.Services.AddScoped<IWareHouseRepository, WarehouseRepository>();
            builder.Services.AddScoped<IWareHouseService, WarehouseService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecretKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });


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
