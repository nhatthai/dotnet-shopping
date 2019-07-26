﻿using Web.Shopping.HttpAggregator.Models;
using System.Threading.Tasks;

namespace Web.Shopping.HttpAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketData> GetByIdAsync(string id);

        Task UpdateAsync(BasketData currentBasket);
    }
}
