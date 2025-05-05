using Nimap_Project.Models;

namespace Nimap_Project.Services
{
    public interface IProductServices
    {
        IEnumerable<Product>GetProducts();
        public Product GetProductById(int id);

        public int AddProduct(Product product);

        public int UpdateProduct(Product product);

        public int DeleteProduct(int id);

    }
}
