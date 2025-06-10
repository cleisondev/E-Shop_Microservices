using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync();
        }

        private IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {
          new Product()
           {
               Id = Guid.Parse("f18b5c28-3f12-4b11-9e8d-44f7dcb5a1e1"),
               Name = "BMW",
               Description = "Carro alemão",
               ImageFile = "carro.jpg",
               Price = 50000,
               Category = new List<string> { "Carro" }
           },
           new Product()
           {
               Id = Guid.Parse("d2f7050f-9125-4ae5-8ea2-8d451a51d8c4"),
               Name = "iPhone 14 Pro",
               Description = "Smartphone da Apple com tecnologia de ponta",
               ImageFile = "iphone.jpg",
               Price = 899990,
               Category = new List<string> { "Eletrônico", "Celular" },
           },
           new Product()
           {
               Id = Guid.Parse("c6f245ed-c4b7-4f9c-8e8a-34771c1b4c01"),
               Name = "Notebook Dell XPS 13",
               Description = "Notebook premium com tela infinita e alto desempenho",
               ImageFile = "notebook.jpg",
               Price = 899990,
               Category = new List<string> { "Eletrônico", "Informática" },
           },
           new Product()
           {
               Id = Guid.Parse("a8df7852-f114-422b-9095-d8a9b93c5d2f"),
               Name = "Violão Yamaha",
               Description = "Violão acústico de excelente qualidade sonora",
               ImageFile = "violao.jpg",
               Price = 120000,
               Category = new List<string> { "Instrumento Musical" },
           },
           new Product()
           {
               Id = Guid.Parse("6a8d360e-b1a7-41c6-98b1-65487b90ddc3"),
               Name = "Smart TV Samsung 55\" 4K",
               Description = "Televisão inteligente com resolução Ultra HD e recursos modernos",
               ImageFile = "tv.jpg",
               Price = 299999,
               Category = new List<string> { "Eletrônico", "TV" }
           },
           new Product()
           {
               Id = Guid.Parse("e0c87029-ef40-45e6-8a8d-3f2853ed2896"),
               Name = "Cafeteira Nespresso",
               Description = "Cafeteira elétrica para cápsulas, prática e moderna",
               ImageFile = "cafeteira.jpg",
               Price = 49999,
               Category = new List<string> { "Eletrodoméstico", "Cozinha" },
           }
        };
    }
}
