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
                orch.Delete();
                orch.Create(new User() { Name = "FromWebPage", Age = 30 });
                var users = orch.Read();
                context.Response.WriteAsync("Users list:\n");
                foreach (User u in users)
                {
                    context.Response.WriteAsync($"{u.Id}\t{u.Name} - {u.Age}\n");
                }

                context.Response.WriteAsync($"-----\n");
                foreach (var u in users)
                {
                    orch.Update(u.Id, new User() { Name="Updated", Age=10 });
                }
                users = orch.Read();
                context.Response.WriteAsync("Users list:\n");
                foreach (User u in users)
                {
                    context.Response.WriteAsync($"{u.Id}\t{u.Name} - {u.Age}\n");
                }
            });

            app.Run();
        }
    }
}