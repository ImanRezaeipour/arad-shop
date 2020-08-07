using System;
using Shopping.Infrastructure.Core.Domain.Values;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Persons.ValueObjects
{
    public class CustomerRecommender : ValueObject<CustomerRecommender>
    {
        protected CustomerRecommender()
        {
        }

        public CustomerRecommender(Guid recommenderId, long code, RecommenderType? recommenderType)
        {
            Code = code;
            RecommenderId = recommenderId;
            RecommenderType = recommenderType;
        }
        public long Code { get; private set; }
        public Guid RecommenderId { get; private set; }
        public RecommenderType? RecommenderType { get; private set; }
        public bool HasValue => (RecommenderId != Guid.Empty || Code != 0 || RecommenderType != null);
    }
}