using Models;

namespace GraphQL_App
{
    public class Subscription
    {
        [Subscribe]
        [Topic(nameof(Mutation.AddProduct))]
        public Product ProductAdded([EventMessage] Product product) => product;

        [Subscribe]
        [Topic(nameof(Mutation.ChangePassword))]
        public User PasswordChanged([EventMessage] User user) => user;
    }
}
