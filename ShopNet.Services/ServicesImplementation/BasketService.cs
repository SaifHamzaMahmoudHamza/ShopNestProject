using AutoMapper;
using ShopNest.Domain.Contracts.RepositoryAbstraction.BasketRepositoryAbstraction;
using ShopNest.Domain.Entities.BasketModule;
using ShopNest.Services.Abstraction.ServiceAbstractions;
using ShopNest.Shared.DTOs.BasketDTOs;
using ShopNet.Services.Exceptions;

namespace ShopNet.Services.ServicesImplementation
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<CustomerBasketDTO?> CreateOrUpdateBasketAsync(CustomerBasketDTO customerBasketDTO)
        {
            var customerBasket = mapper.Map<CustomerBasketDTO, CustomerBasket>(customerBasketDTO);
            var CreatedOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            return mapper.Map<CustomerBasket, CustomerBasketDTO>(CreatedOrUpdatedBasket!);
        }

        public async Task<bool> DeleteBasketAsync(string basketId) =>
            await basketRepository.DeleteBasketAsync(basketId);



        public async Task<CustomerBasketDTO?> GetBasketByIdAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket == null)
                throw new BasketNotFoundException(basketId);

            return mapper.Map<CustomerBasket, CustomerBasketDTO>(basket!);
        }
    }
}
