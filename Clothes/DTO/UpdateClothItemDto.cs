﻿using Clothes.Models;

namespace Clothes.DTO
{
    public class UpdateClothItemDto
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Quanitity { get; set; }
        public bool hasDiscount { get; set; }
        public int discount { get; set; }
        public int price { get; set; }

    }
}
