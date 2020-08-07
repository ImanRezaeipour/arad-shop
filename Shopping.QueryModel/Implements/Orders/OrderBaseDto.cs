using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.Implements.Orders
{
    public class OrderBaseDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int? TimeLeft { get; set; }
        public bool IsConvertToArea { get; set; }
    }
}