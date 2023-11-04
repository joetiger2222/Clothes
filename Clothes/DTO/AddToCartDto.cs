using Clothes.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothes.DTO
{
    public class AddToCartDto
    {
        
        public Guid UserId { get; set; }

        public int ClothId { get; set; }
        public int Quantity { get; set; }

        
    }
}
