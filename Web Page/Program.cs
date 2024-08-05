using Entities;

namespace Web_Page
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationContext>(ServiceLifetime.Singleton);
            var app = builder.Build();

            
            app.Run(async (context) =>
            {

            });

            app.Run();
        }
    }
}