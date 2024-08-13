using Models;
using System.Runtime.CompilerServices;

namespace GraphQL_App
{
    public class Subscription
    {
        [Subscribe]
        [Topic(nameof(Mutation.AddProduct))]
        public Product ProductAdded([EventMessage] Product product) => product;

        [Topic(nameof(Mutation.ChangePassword))]
        [Subscribe(With = nameof(CreatePasswordChangedStream))]
        public User PasswordChanged([EventMessage] User user) => user;

        public async IAsyncEnumerable<User> CreatePasswordChangedStream(
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await Task.Delay(150, cancellationToken);
            yield return new User
            {
                Login = "someLogin 1",
                Password = "someNewPassword"
            };

            await Task.Delay(150, cancellationToken);
            yield return new User
            {
                Login = "someLogin 2",
                Password = "someNewPassword"
            };

            await Task.Delay(150, cancellationToken);
            yield return new User
            {
                Login = "someLogin 3",
                Password = "someNewPassword"
            };
        }
    }
}
