using Marten.Schema;

namespace CatalogAPI.Data
{
    public class InitialCatalogData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetDefaultProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetDefaultProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung Galaxy S25",
                    Category = new List<string> { "Mobile", "Tab" },
                    Description = "Samsung Galaxy S25",
                    ImageFile = "https://static0.howtogeekimages.com/wordpress/wp-content/uploads/2025/01/samsung-galaxy-s25-ultra-1.jpg?w=1600&h=900&fit=crop"
                },

                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple iPhone 16",
                    Category = new List<string> { "Mobile", "Electronics" },
                    Description = "Apple iPhone 16 with advanced camera and performance",
                    ImageFile = "https://example.com/images/iphone16.jpg"
                },

                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Google Pixel 9",
                    Category = new List<string> { "Mobile", "Electronics" },
                    Description = "Google Pixel 9 — clean Android experience and excellent camera",
                    ImageFile = "https://example.com/images/pixel9.jpg"
                },

                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "OnePlus 13 Pro",
                    Category = new List<string> { "Mobile", "Electronics" },
                    Description = "OnePlus 13 Pro with high refresh display and fast charging",
                    ImageFile = "https://example.com/images/oneplus13pro.jpg"
                },

                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sony Xperia 1 VI",
                    Category = new List<string> { "Mobile", "Camera" },
                    Description = "Sony Xperia 1 VI — pro-grade imaging in a smartphone form factor",
                    ImageFile = "https://example.com/images/xperia1vi.jpg"
                }
            };
        }
    }
}
