﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace ECommerce.ProductCatalog.Model
{
    public interface IProductCatalogService : IService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task AddProduct(Product product);

        Task<Product> GetProduct(Guid productId);
    }
}