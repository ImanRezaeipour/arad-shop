using AutoMapper;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.QueryModel.QueryModels.OrdersSuggestions;

namespace Shopping.QueryService.Implements.OrdersSuggestions
{
    public static class OrderSuggestionMapper
    {
        public static IOrderSuggestionDto ToDo(this OrderSuggestion src)
        {
            return Mapper.Map<IOrderSuggestionDto>(src);
        }
    }
}