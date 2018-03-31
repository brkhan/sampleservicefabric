using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting;

namespace ECommerce.CheckoutService.Model
{
    public interface ICheckoutService : IService
    {
        Task<CheckoutSummary> Checkout(string userId);

        Task<IEnumerable<CheckoutSummary>> GetOrderHitory(string userId);
    }

    public class CheckoutSummary
    {
        public List<CheckoutProduct> Products { get; set; }

        public double TotalPrice { get; set; }

        public DateTime Date { get; set; }
    }

    public class CheckoutProduct
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
