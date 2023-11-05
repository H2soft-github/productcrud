using MediaExpertWebApi.Interfaces;
using MediaExpertWebApi.Models;

namespace MediaExpertWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseInMemory databaseInMemory;
        public ProductService(DatabaseInMemory databaseInMemory)
        {
            this.databaseInMemory = databaseInMemory;
        }

        public IEnumerable<Product> GetProducts()
        {
            return databaseInMemory.Products.Get().OrderBy(x => x.Name)
                .ThenBy(x => x.Id);
        }

        public void AddProduct(Product product)
        {
            databaseInMemory.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            databaseInMemory.Products.Update(product);
        }

        public Product GetProduct(int id)
        {
            return databaseInMemory.Products.Get(id);
        }

        public void DeleteProduct(int id)
        {
            databaseInMemory.Products.Delete(id);
        }
    }
}
