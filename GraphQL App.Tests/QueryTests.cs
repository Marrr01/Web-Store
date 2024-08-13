using CookieCrumble;
using Xunit;

namespace GraphQL_App.Tests
{
    public class QueryTests
    {
        [Fact]
        public async Task GetUsers_CorrectQuery_ReturnsProductsInUsersBaskets()
        {
            await using var result = await TestServices.ExecuteRequestAsync(b => b.SetQuery(
                """
                query {
                  users {
                    login
                    password
                    baskets {
                      basketProducts {
                        productAmount
                        product {
                          name
                          price
                        }
                      }
                    }
                  }
                }
                """));
            result.MatchSnapshot();
        }
    }
}