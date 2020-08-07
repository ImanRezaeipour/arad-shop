using System;
using Shopping.Commands.Commands.Discounts.Commands.Abstract;

namespace Shopping.Commands.Commands.Discounts.Commands
{
    public class CreateDiscountPercentCommand: DiscountBaseCommand
    {
        public float Percent { get; set; }
        public int MaxOrderCount { get; set; }
        public int MaxProductCount { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}