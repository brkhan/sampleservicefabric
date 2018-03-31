using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Newtonsoft.Json;
using UserActor.Interfaces;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        [HttpGet("{userId}")]
        public async Task<ApiBasket> Get(string userId)
        {
            IUserActor actor = GetActor(userId);

            Dictionary<Guid, int> products = await actor.GetBasket();

            return new ApiBasket()
            {
                UserId = userId,
                Items = products.Select(
                    p => new ApiBasketItem {ProductId = p.Key.ToString(), Quantity = p.Value}).ToArray()
            };
        }

        [HttpPost("{userId}")]
        public async Task Add(string userId, [FromBody] ApiBasketAddRequest request)
        {
            try
            {
                IUserActor actor = GetActor(userId);
                if (request == null)
                {
                    request = new ApiBasketAddRequest
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1
                    };
                }
                await actor.AddToBasket(request.ProductId, request.Quantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpDelete("{userId}")]
        public async Task Delete(string userId)
        {
            IUserActor actor = GetActor(userId);

            await actor.ClearBasket();
        }

        private IUserActor GetActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(new ActorId(userId), new Uri("fabric:/Ecommerce/UserActorService"));
        }
    }

    public class ApiBasket
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("item")]
        public ApiBasketItem[] Items { get; set; }
    }

    public class ApiBasketItem
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }

    public class ApiBasketAddRequest
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}