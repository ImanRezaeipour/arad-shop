namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorRawDto
    {
        long Id { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }
        string Description { get; set; }
        string ProductName { get; set; }
        string ProductImage { get; set; }
        string BrandName { get; set; }
        bool HaveDiscount { get; set; }
    }
}