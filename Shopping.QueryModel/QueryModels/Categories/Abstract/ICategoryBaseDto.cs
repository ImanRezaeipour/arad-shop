using System;

namespace Shopping.QueryModel.QueryModels.Categories.Abstract
{
    public interface ICategoryBaseDto
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Order { get; set; }
        bool IsRoot { get; set; }
        bool IsActive { get; set; }
    }
}