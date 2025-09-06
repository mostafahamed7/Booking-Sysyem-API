using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.DTOs;

namespace Services
{
    internal class BasketServices(IBasketRepository basketRepository, IMapper mapper) : IBasketServices
    {
        public async Task<bool> DeleteBasketAsync(string id) => await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = mapper.Map<Basket>(basket);

            var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);

            return updatedBasket is null ? throw new Exception("Can not Updated Basket") : mapper.Map<BasketDTO>(updatedBasket);
        }
    }
}
