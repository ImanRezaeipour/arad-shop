namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IBankAccountDto
    {
        string Iban { get; set; }
        string AccountOwnerName { get; set; }
        string AccountNumber { get; set; }
    }
}