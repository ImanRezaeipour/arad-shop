using System;

namespace Shopping.QueryModel.Implements.Brands
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public bool IsActive { get; set; }
    }
}