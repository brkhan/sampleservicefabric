using System;
using ECommerce.ProductCatalog;
using ECommerce.ProductCatalog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommmerce.ProductCatalog
{
    interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task AddProduct(Product product);

        Task<Product> GetProduct(Guid productId);
    }
}
