//Step 03) Add the using statement below
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("OriginPolicy", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();//"http://johnblackcoding.com"
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Step 04) Add the ResourceContext Service 
            //This will initialize the database connection to be used in the controllers
            builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));
                    //The string above should match the connectionString name in appsettings.json
                }
              );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors();    

            app.Run();
        }
    }
}