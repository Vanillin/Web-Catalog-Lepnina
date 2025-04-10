﻿namespace Application.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdSection { get; set; }
    }
}
