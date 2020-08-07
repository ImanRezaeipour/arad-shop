namespace Shopping.QueryModel.QueryModels.OrdersSuggestions
{
    public interface IOrderSuggestionItemTypeCountDto
    {
        int AcceptedCount { get; set; }
        int RejectedCount { get; set; }
        int AlternativeCount { get; set; }
    }
}