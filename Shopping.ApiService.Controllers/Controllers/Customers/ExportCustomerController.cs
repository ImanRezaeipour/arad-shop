using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Castle.Components.DictionaryAdapter;
using Shopping.Infrastructure.Excel;
using Shopping.Infrastructure.Helper;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Persons.Customer;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Customers
{
    public class ExportCustomerController:ApiControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        public ExportCustomerController(ICustomerQueryService customerQueryService)
        {
            _customerQueryService = customerQueryService;
        }

        public HttpResponseMessage Get( )
        {
            List<ICustomerExcelDto> result = new EditableList<ICustomerExcelDto>();
            var customer = _customerQueryService.GetAll();
            result.AddRange(customer.Select(p => new CustomerExcelDto
            {
                FirstName=p.FirstName,
                LastName = p.LastName,
                EmailAddress = p.EmailAddress,
                IsActive = p.IsActive?"فعال":"غیر فعال",
              
            }));
            HttpResponseMessage response;
            DataTable dt = new DataTable();
            dt = result.ToDataTableExtension();
            MemoryStream ms = new MemoryStream();
            try
            {
                ExcelReader.WriteExcel(ms, dt);
                ms.Position = 0;

                response = new HttpResponseMessage
                {
                    Content = new StreamContent(ms)
                };

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "fefef.xlsx"
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage
                {
                    Content = new StringContent("Error: " + ex.Message)
                };
            }
            return response;
        }

    }
}