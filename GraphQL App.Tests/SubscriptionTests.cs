using CookieCrumble;
using HotChocolate.Execution;
using Xunit;

namespace GraphQL_App.Tests
{
    public class SubscriptionTests
    {
        [Fact]
        public async Task PasswordChanged_CorrectQuery_ReturnsTwoUsers()
        {
            using var cts = new CancellationTokenSource(2500);

            await using var result = await TestServices.ExecuteRequestAsync(b => b.SetQuery(
                """
                subscription t {
                  passwordChanged {
                    login
                    password
                  }
                }
                """
                ));

            var snapshot = new Snapshot();
            var count = 0;

            await foreach (var item in result.ExpectResponseStream()
                .ReadResultsAsync().WithCancellation(cts.Token))
            {
                snapshot.Add(item, $"Result {++count}");

                if (count == 2)
                {
                    break;
                }
            }

            snapshot.Match();
        }
    }
}
