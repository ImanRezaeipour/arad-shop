using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Orders.Abstract
{
    public interface IOrderBaseDto
    {
        long Id { get; set; }
        string Description { get; set; }
        DateTime CreationTime { get; set; }
        DateTime ExpireOpenTime { get; set; }
        DateTime? ResponseExpireTime { get; set; }
        DateTime? SuggestionTime { get; set; }
        OrderStatus OrderStatus { get; set; }
        OrderType OrderType { get; set; }
        int? TimeLeft { get; set; }
        bool IsExpired { get; set; }
    }
}