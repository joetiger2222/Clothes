namespace Clothes.Models
{
    public class ClotheItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Quanitity { get; set; }
        public bool hasDiscount { get; set; }
        public int discount { get; set; }
        public int price { get; set; }
        public Category Category { get; set; }
        public List<Image> ImgsPaths { get; set; }

    }
}
