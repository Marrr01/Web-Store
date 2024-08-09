using HotChocolate.Execution.Processing;
using Models;

namespace GraphQL_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationContext>(ServiceLifetime.Scoped);
            builder.Services
                   .AddGraphQLServer()
                   .RegisterDbContext<ApplicationContext>(DbContextKind.Synchronized)
                   .AddQueryType<Query>()
                   .AddMutationType<Mutation>()
                   .AddMutationConventions()
                   .AddSubscriptionType<Subscription>()
                   .AddInMemorySubscriptions()
                   .AddFiltering()
                   .AddSorting()
                   .AddProjections();

            var app = builder.Build();
            app.UseWebSockets();
            app.MapGraphQL();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/");
            });
            app.Run();
        }
    }
}