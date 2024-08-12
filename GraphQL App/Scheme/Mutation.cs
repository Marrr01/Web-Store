using Database;
using HotChocolate.Subscriptions;
using Models;

namespace GraphQL_App
{
    public class Mutation
    {
        [UseProjection]
        [Error(typeof(NullReferenceException))]
        public async Task<User?> ChangePassword(ApplicationContext db,
                                                string login,
                                                string oldPass,
                                                string newPass,
                                                [Service] ITopicEventSender sender)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == login &&
                                                    u.Password == oldPass);
            user.Password = newPass;
            db.SaveChanges();
            await sender.SendAsync(nameof(ChangePassword), user);
            return user;
        }

        [UseProjection]
        public async Task<Product?> AddProduct(ApplicationContext db,
                                               Product newProduct,
                                               [Service] ITopicEventSender sender)
        {
            db.Products.Add(newProduct);
            db.SaveChanges();
            await sender.SendAsync(nameof(AddProduct), newProduct);
            return newProduct;
        }
    }
}
