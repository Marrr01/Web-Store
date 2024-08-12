using Snapshooter.Xunit;
using Xunit;

namespace GraphQL_App.Tests
{
    public class QueryTests
    {
        [Fact]
        public async Task SchemaChangeTest()
        {
            var schema = await TestServices.Executor.GetSchemaAsync(default);
            schema.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task GetUsers()
        {
            var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ users { login password } }"));

            result.MatchSnapshot();
        }
    }
}