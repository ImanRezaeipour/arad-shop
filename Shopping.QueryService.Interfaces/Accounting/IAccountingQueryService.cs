using System;

namespace Shopping.QueryService.Interfaces.Accounting
{
    public interface IAccountingQueryService
    {
        object GetShopRemain(Guid userId,string fromDate,string toDate);
        object GetShopSettlement(Guid userId, string fromDate, string toDate);
    }
}