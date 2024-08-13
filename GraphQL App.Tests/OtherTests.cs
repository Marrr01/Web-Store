using CookieCrumble;
using Xunit;

namespace GraphQL_App.Tests
{
    public class OtherTests
    {
        [Fact]
        public async Task SchemaChangeTest()
        {
            var schema = await TestServices.Executor.GetSchemaAsync(default);
            schema.MatchSnapshot();
        }
    }
}
