using System.ComponentModel.DataAnnotations.Schema;

namespace Clothes.Models
{
    public class Cart
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(ClotheItem))]
        public int ClothId { get; set; }
        public int Quantity { get; set; }

        public ClotheItem ClotheItem { get; set; }
    }
}
