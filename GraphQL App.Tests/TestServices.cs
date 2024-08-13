using Database;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Moq;

namespace GraphQL_App.Tests
{
    public static class TestServices
    {
        static TestServices()
        {
            var mock = new Mock<ApplicationContext>();
            mock.SetupGet(db => db.Users).Returns(GetQueryableMockDbSet(GetUsers()));
            mock.SetupGet(db => db.Baskets).Returns(GetQueryableMockDbSet(GetBaskets()));
            mock.SetupGet(db => db.BasketsProducts).Returns(GetQueryableMockDbSet(GetBasketProducts()));
            mock.SetupGet(db => db.Products).Returns(GetQueryableMockDbSet(GetProducts()));

            Services = new ServiceCollection()
                //.AddDbContext<ApplicationContext>()
                .AddScoped((x) => mock.Object)
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

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        public static IServiceProvider Services { get; }

        public static RequestExecutorProxy Executor { get; }

        public static async Task<IExecutionResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
        {
            var scope = Services.CreateAsyncScope();

            var requestBuilder = new QueryRequestBuilder();
            requestBuilder.SetServices(scope.ServiceProvider);
            configureRequest(requestBuilder);
            var request = requestBuilder.Create();

            var result = await Executor.ExecuteAsync(request, cancellationToken);
            result.RegisterForCleanup(scope.DisposeAsync);
            return result;
        }

        #region Данные для мока
        private static List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Login = "login 1",
                    Password = "password",
                    Baskets = GetBaskets()
                },
                new User()
                {
                    Login = "login 2",
                    Password = "password",
                    Baskets = GetBaskets()
                }
            };
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Price = 10
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Price = 20
                }
            };
        }

        private static List<BasketProduct> GetBasketProducts()
        {
            return new List<BasketProduct>()
            {
                new BasketProduct()
                {
                    Product = GetProducts()[0],
                    ProductAmount = 1
                },
                new BasketProduct()
                {
                    Product = GetProducts()[1],
                    ProductAmount = 2
                }
            };
        }

        private static List<Basket> GetBaskets()
        {
            return new List<Basket>()
            {
                new Basket()
                {
                    Id = Guid.NewGuid(),
                    BasketProducts = GetBasketProducts()
                },
                new Basket()
                {
                    Id = Guid.NewGuid(),
                    BasketProducts = GetBasketProducts()
                }
            };
        }
        #endregion
    }
}
