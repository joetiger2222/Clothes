using Clothes.Data;
using Clothes.DTO;
using Clothes.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes.Repositories
{
    public class SqlCartRepository : ICartRepository
    {
        private readonly DataDbContext dbContext;

        public SqlCartRepository(DataDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Cart> AddToCart(Cart cart)
        {
            await dbContext.Cart.AddAsync(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> DeleteCartItem(Guid itemId)
        {
            var itemToDelete = await dbContext.Cart.FirstOrDefaultAsync(c=>c.id== itemId);
            if(itemToDelete is null)
            {
                return null;
            }
            dbContext.Cart.Remove(itemToDelete);
            await dbContext.SaveChangesAsync();
            return itemToDelete;
        }

        public async Task<List<Cart>> GetCartItems(Guid userId)
        {
            var cartItems = await dbContext.Cart
                .Where(cart=>cart.UserId==userId)
                
                .Include(c=>c.ClotheItem.Category)
                .Include(c=>c.ClotheItem.ImgsPaths)
                .Include("ClotheItem").ToListAsync();
            return cartItems;
        }

        public async Task<Cart> UpdateCartItem(Cart cart, Guid itemId)
        {
            var itemToUpdate=await dbContext.Cart.FirstOrDefaultAsync(x=>x.id==itemId);
            if (itemToUpdate==null)
            {
                return null;
            }
            itemToUpdate.Quantity=cart.Quantity;
            await dbContext.SaveChangesAsync();
            return cart;
        }
    }
}
