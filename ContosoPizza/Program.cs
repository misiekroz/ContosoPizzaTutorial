
using ContosoPizza.Models;
using ContosoPizza.Models.Users;
using ContosoPizza.Services;

namespace ContosoPizza
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // setup dependency injection for all controllers
            builder.Services.AddSingleton<IContosoService<Pizza>, ContosoService<Pizza>>();
            builder.Services.AddSingleton<IContosoService<Order>, ContosoService<Order>>();
            builder.Services.AddSingleton<IContosoService<User>, ContosoService<User>>();
            builder.Services.AddSingleton<IContosoService<PremiumUser>, ContosoService<PremiumUser>>();

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

            app.Run();
        }
    }
}
