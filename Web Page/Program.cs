using Entities;
using Entities.Models;
using Entities.Orchestrators;

namespace Web_Page
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationContext>(ServiceLifetime.Singleton);
            builder.Services.AddSingleton<UserOrch>();
            var app = builder.Build();

            app.Run(async (context) =>
            {
                var orch = app.Services.GetService<UserOrch>();
            });

            app.Run();
        }
    }
}