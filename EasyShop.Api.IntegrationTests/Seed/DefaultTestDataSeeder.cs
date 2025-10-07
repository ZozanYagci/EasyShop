using Bogus;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;


namespace EasyShop.Api.IntegrationTests.Seed
{
    public class DefaultTestDataSeeder : ITestDataSeeder
    {
        public void Seed(Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));


            var users = new List<AuthUser>
            {
                new AuthUser { Id=1, FirstName="Clarice", LastName="Starling", Email="cstarling@example.com", PasswordHash=new byte[0], PasswordSalt=new byte[0], Status=true },
                new AuthUser { Id=2, FirstName="Evey", LastName="Hammond", Email="evey@example.com", PasswordHash=new byte[0], PasswordSalt=new byte[0], Status=true },
                new AuthUser { Id=3, FirstName="Dude", LastName="Lebowski", Email="dude@example.com", PasswordHash=new byte[0], PasswordSalt=new byte[0], Status=true }
            };
            context.AuthUsers.AddRange(users);
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { Id=1,Name="Erkek Kazak", ImageUrl="https://picsum.photos/200" },
                new Product { Id=2,Name="Kadın Tişört", ImageUrl="https://picsum.photos/200" },
                new Product { Id=3,Name="Çocuk Ayakkabı", ImageUrl="https://picsum.photos/200" },
                new Product { Id=4,Name="Bluetooth Kulaklık", ImageUrl="https://picsum.photos/200"},
                new Product { Id=5,Name="Laptop Çantası", ImageUrl="https://picsum.photos/200"}
            };
            context.Products.AddRange(products);
            context.SaveChanges();


            var prices = products.Select((p, idx) => new ProductPrice
            {
                ProductId = p.Id,
                Price = (idx + 1) * 100,
                IsCurrent = true
            }).ToList();
            context.ProductPrices.AddRange(prices);
            context.SaveChanges();
        }
    }
}
