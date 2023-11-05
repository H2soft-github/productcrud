using MediaExpertWebApi.Models;

namespace MediaExpertWebApi.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public void AddProduct(Product product);
        public Product GetProduct(int id);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int id);
    }
}
