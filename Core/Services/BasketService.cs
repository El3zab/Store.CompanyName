using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundException(id);

            var result = mapper.Map<BasketDto>(basket);

            return result;
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);
            basket = await basketRepository.UpdateBasketAsync(basket);

            if (basket is null) throw new BasketCreateOrUpdateBadException();

            var result = mapper.Map<BasketDto>(basket);
            
            return result;
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            var flag = basketRepository.DeleteBasketAsync(id);
            if (flag == null) throw new BasketDeleteBadRequestException();

            return flag;
        }

    }
}
