using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController(IServiceManger _serviceManger) : ApiController
    {
        // Get Basket by Id
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var basket = await _serviceManger.BasketServices.GetBasketAsync(id);
            return Ok(basket);
        }

        // Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> UpdateBasket(BasketDTO basketDto)
        {
            var updatedBasket = await _serviceManger.BasketServices.UpdateBasketAsync(basketDto);
            return Ok(updatedBasket);
        }

        // Delete Basket
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await _serviceManger.BasketServices.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
