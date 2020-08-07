using Shopping.QueryModel.QueryModels.Persons.Customer;

namespace Shopping.QueryModel.Implements.Persons
{
    public class CustomerExcelDto: ICustomerExcelDto
    {
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string IsActive { get; set; }
    }
}