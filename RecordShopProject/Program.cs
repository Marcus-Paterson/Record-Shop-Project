
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RecordShopProject.Repository;
using RecordShopProject.Service;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecordShopProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddScoped<IRecordsRepository, RecordsRepository>();
            builder.Services.AddScoped<IRecordsService, RecordsService>();
            builder.Services.AddDbContext<RecordShopDBContext>(options => 
            {
                if (builder.Environment.IsDevelopment())
                {
                    string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
                    var connection = new SqliteConnection(connectionString);
                    connection.Open();
                    options.UseSqlite(connection);
                    
                } else
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            }
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            using (IServiceScope scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RecordShopDBContext>();

                if (app.Environment.IsDevelopment()) db.Database.EnsureCreated();
                else db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            /*if (app.Environment.IsDevelopment())
            {*/
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
