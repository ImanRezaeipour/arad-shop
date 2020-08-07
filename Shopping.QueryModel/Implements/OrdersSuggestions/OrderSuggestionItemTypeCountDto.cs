using Shopping.QueryModel.QueryModels.OrdersSuggestions;

namespace Shopping.QueryModel.Implements.OrdersSuggestions
{
    public class OrderSuggestionItemTypeCountDto: IOrderSuggestionItemTypeCountDto
    {
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public int AlternativeCount { get; set; }
    }
}