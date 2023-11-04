using Clothes.DTO;
using Clothes.Models;

namespace Clothes.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddToCart(Cart cart);
        Task<List<Cart>>GetCartItems(Guid userId);
        Task<Cart>UpdateCartItem(Cart cart,Guid itemId);
        Task<Cart>DeleteCartItem(Guid itemId);
    }
}
