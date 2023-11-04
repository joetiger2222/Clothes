using AutoMapper;
using Clothes.DTO;
using Clothes.Models;
using Clothes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartController(ICartRepository cartRepository,IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody]AddToCartDto addToCartDto)
        {
            var AddedToCart=await cartRepository.AddToCart(mapper.Map<Cart>(addToCartDto));
            return Ok(AddedToCart);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult>GetCartItems(Guid userId)
        {
            var cartItems=await cartRepository.GetCartItems(userId);
            return Ok(mapper.Map<List<CartDto>>(cartItems));
        }

        [HttpPut]
        [Route("{itemId}")]
        public async Task<IActionResult>UpdateCartItem(Guid itemId,UpdateCartItemDto updateCartItemDto)
        {
            var updatedItem=await cartRepository.UpdateCartItem(mapper.Map<Cart>(updateCartItemDto),itemId);
            if(updatedItem is null)
            {
                return NotFound();
            }
            return Ok(new { message = "Item Updated Successfully" });
        }

        [HttpDelete]
        [Route("{itemId}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute]Guid itemId)
        {
            var deletedItem=await cartRepository.DeleteCartItem(itemId);
            if(deletedItem is null)
            {
                return NotFound();
            }
            return Ok(new { message = "Item Deleted Successfully" });
        }
    }
}
