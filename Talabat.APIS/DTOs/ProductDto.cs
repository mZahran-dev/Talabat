﻿using Talabat.Core.Entities;

namespace Talabat.APIS.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //Relations 
        public int BrandId { get; set; }
        public string ProductBrand { get; set; }
        public int CategoryId { get; set; }
        public string ProductCategory { get; set; }
    }
}
