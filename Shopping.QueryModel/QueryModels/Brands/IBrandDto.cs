using System;

namespace Shopping.QueryModel.QueryModels.Brands
{
    public interface IBrandDto
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string LatinName { get; set; }
        bool IsActive { get; set; }
    }
}