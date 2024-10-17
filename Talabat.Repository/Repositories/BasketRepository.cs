using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
       private readonly StackExchange.Redis.IDatabase _database;
       public BasketRepository(IConnectionMultiplexer connection)
       {
            _database = connection.GetDatabase();
       }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
           return await _database.KeyDeleteAsync(BasketId); 
             
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Basketid)
        {
            var basket = await _database.StringGetAsync(Basketid);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreateOrUpdatedBasket = _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (CreateOrUpdatedBasket is null) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
