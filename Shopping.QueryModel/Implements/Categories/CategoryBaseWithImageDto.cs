using System;
using Shopping.QueryModel.QueryModels.Categories;
using Shopping.QueryModel.QueryModels.Categories.Abstract;

namespace Shopping.QueryModel.Implements.Categories
{
    public class CategoryBaseWithImageDto: ICategoryBaseWithImageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsRoot { get; set; }
        public bool IsActive { get; set; }
        public ICategoryImageDto CategoryImage { get; set; }
    }
}