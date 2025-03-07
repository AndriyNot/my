﻿namespace ExamProject.API.Models.Dtos
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }
        public ProductDescription? Description { get; set; }
    }
}
