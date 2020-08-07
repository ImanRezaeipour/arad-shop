using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.DomainModel.Aggregates.FakeIos.Orders;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.FakeIos.Orders;
using Shopping.QueryService.Interfaces.FakeIos;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.FakeIos
{
    public class FakeIosQueryService : IFakeIosQueryService
    {
        private readonly IReadOnlyRepository<FakeOrderIos, Guid> _repository;
        public FakeIosQueryService(IReadOnlyRepository<FakeOrderIos, Guid> repository)
        {
            _repository = repository;
        }
        public IList<IFakeOrderIosDto> GetOrders(FakeOrderIosState? state)
        {
            if (state == null)
            {
                var result = _repository.AsQuery().ToList().Select(p => p.ToDto()).ToList();
                return result;
            }
            var result1 = _repository.AsQuery().Where(p => p.State == state).ToList().Select(p => p.ToDto()).ToList();
            return result1;
        }
        public int GetOrdersCount(FakeOrderIosState? state)
        {
            if (state == null)
            {
                var result = _repository.AsQuery().Count();
                return result;
            }
            var result1 = _repository.AsQuery().Count(p => p.State == state);
            return result1;
        }
    }
}