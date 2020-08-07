using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Products.ValueObjects
{
    public class FakeProductDiscount : ValueObject<FakeProductDiscount>
    {
        protected FakeProductDiscount() { }
        public FakeProductDiscount(int from, int to)
        {
            From = from;
            To = to;
        }
        public int From { get; private set; }
        public int To { get; private set; }
    }
}