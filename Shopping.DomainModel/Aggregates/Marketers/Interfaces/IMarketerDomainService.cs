using Shopping.DomainModel.Aggregates.Marketers.Aggregates;

namespace Shopping.DomainModel.Aggregates.Marketers.Interfaces
{
    public interface IMarketerDomainService
    {
        void CheckMarketerActive(Marketer marketer);
        void CheckMaxMarketerAllowedIsEnough(Marketer marketer);
    }
}