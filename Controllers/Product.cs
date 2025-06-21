using PriceComparisiontool.Models;
using System.Collections.Concurrent;
using PriceComparisiontool.Models;
namespace PriceComparisiontool.Controllers
{
    public class ProductService
    {
        private readonly ConcurrentDictionary<string, Product> _products = new();

        public Product? GetByName(string name)
        {
            _products.TryGetValue(name.ToLower(), out var product);
            return product;
        }

        public void Add(Product product)
        {
            product.LastUpdated = DateTime.UtcNow;
            _products[product.Name.ToLower()] = product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Values;
        }
    }

}
