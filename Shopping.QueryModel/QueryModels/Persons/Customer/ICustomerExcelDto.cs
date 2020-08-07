using System.ComponentModel;

namespace Shopping.QueryModel.QueryModels.Persons.Customer
{
    public interface ICustomerExcelDto
    {
        [Description("نام")]
        string FirstName { get; set; }
        [Description("نام خانوادگی")]
        string LastName { get; set; }
        [Description("ایمیل")]
        string EmailAddress { get; set; }
        [Description("فعال")]
        string IsActive { get; set; }
      
    }
}