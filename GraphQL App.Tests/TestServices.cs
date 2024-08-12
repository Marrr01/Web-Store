using Database;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace GraphQL_App.Tests
{
    public static class TestServices
    {
        static TestServices()
        {
            Services = new ServiceCollection()
                .AddDbContext<ApplicationContext>()
                .AddSingleton(
                    sp => new RequestExecutorProxy(
                        sp.GetRequiredService<IRequestExecutorResolver>(),
                        Schema.DefaultName))

                .AddGraphQLServer()
                .RegisterDbContext<ApplicationContext>(DbContextKind.Synchronized)
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddMutationConventions()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions()
                .AddFiltering()
                .AddSorting()
                .AddProjections()

                .Services.BuildServiceProvider();

            Executor = Services.GetRequiredService<RequestExecutorProxy>();
        }

        public static IServiceProvider Services { get; }

        public static RequestExecutorProxy Executor { get; }

        public static async Task<string> ExecuteRequestAsync(
            Action<IQueryRequestBuilder> configureRequest,
            CancellationToken cancellationToken = default)
        {
            await using var scope = Services.CreateAsyncScope();

            var requestBuilder = new QueryRequestBuilder();
            requestBuilder.SetServices(scope.ServiceProvider);
            configureRequest(requestBuilder);
            var request = requestBuilder.Create();

            await using var result = await Executor.ExecuteAsync(request, cancellationToken);

            result.ExpectQueryResult();

            return result.ToJson();
        }

        public static async IAsyncEnumerable<string> ExecuteRequestAsStreamAsync(
            Action<IQueryRequestBuilder> configureRequest,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await using var scope = Services.CreateAsyncScope();

            var requestBuilder = new QueryRequestBuilder();
            requestBuilder.SetServices(scope.ServiceProvider);
            configureRequest(requestBuilder);
            var request = requestBuilder.Create();

            await using var result = await Executor.ExecuteAsync(request, cancellationToken);

            await foreach (var element in result.ExpectResponseStream().ReadResultsAsync().WithCancellation(cancellationToken))
            {
                await using (element)
                {
                    yield return element.ToJson();
                }
            }
        }
    }
}
