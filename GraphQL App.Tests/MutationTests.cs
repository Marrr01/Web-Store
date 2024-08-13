using CookieCrumble;
using Xunit;

namespace GraphQL_App.Tests
{
    public class MutationTests
    {
        [Fact]
        public async Task ChangePassword_CorrectQuery_ReturnsUpdatedUser()
        {
            await using var result = await TestServices.ExecuteRequestAsync(b => b.SetQuery(
            """
            mutation {
              changePassword(input: { login: "login 1", newPass: "newPassword", oldPass: "password" }) {
                user {
                  login
                  password
                }
              }
            }
            """));
            result.MatchSnapshot();
        }

        [Fact]
        public async Task ChangePassword_IncorrectOldPasswordQuery_ReturnsNull()
        {
            await using var result = await TestServices.ExecuteRequestAsync(b => b.SetQuery(
            """
            mutation {
              changePassword(input: { login: "login 1", newPass: "newPassword", oldPass: "incorrectPassword" }) {
                user {
                  login
                  password
                }
              }
            }
            """));
            result.MatchSnapshot();
        }
    }
}
