using Clothes.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothes.DTO
{
    public class CartDto
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }

        public int Quantity { get; set; }

        public ClotheItem ClotheItem { get; set; }
    }
}
