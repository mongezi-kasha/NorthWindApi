using Microsoft.EntityFrameworkCore;
using NorthWind.Services;

namespace Northwind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var configurationRoot = builder.Configuration.AddJsonFile("appsettings.json").Build();

            // Add services to the container.

            string dbConnection = configurationRoot.GetConnectionString("DbConnection");
            string newConn = configurationRoot.GetValue<string>("ConnectionStrings:DbConnection");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<NorthWind.DAL.NorthWindContext>(o => o.UseSqlServer(dbConnection));
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //builder.Services.AddScoped<IEmployeeService, DummyEmployeeService>();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}