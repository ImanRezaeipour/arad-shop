using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Comments.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.Factors;
using Shopping.QueryModel.QueryModels.Factors;
using Shopping.QueryService.Interfaces.Factors;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Factors
{
    public class FactorQueryService : IFactorQueryService
    {
        private readonly IReadOnlyRepository<Factor, long> _repository;
        private readonly IReadOnlyRepository<Shop, Guid> _shopRepository;
        private readonly IReadOnlyRepository<Customer, Guid> _customerRepository;
        private readonly IReadOnlyRepository<Comment, Guid> _commentRepository;
        private readonly IReadOnlyRepository<ApplicationSetting, Guid> _applicationSettingRepository;
        public FactorQueryService(IReadOnlyRepository<Factor, long> repository, IReadOnlyRepository<Shop, Guid> shoprepository, IReadOnlyRepository<Customer, Guid> customeRepository, IReadOnlyRepository<Comment, Guid> commentRepository, IReadOnlyRepository<ApplicationSetting, Guid> applictionSettingRepository)
        {
            _repository = repository;
            _shopRepository = shoprepository;
            _customerRepository = customeRepository;
            _commentRepository = commentRepository;
            _applicationSettingRepository = applictionSettingRepository;
        }
        public async Task<MobilePagingResultDto<IFactorWithCustomerDto>> GetShopFactors(Guid userId, PagedInputDto pagedInput)
        {
            var shop = await _shopRepository.AsQuery().SingleOrDefaultAsync(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var data = _repository.AsQuery().Where(p => p.Shop.Id == shop.Id);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithCustomerDto())
                .ToList();
            return new MobilePagingResultDto<IFactorWithCustomerDto>
            {
                Result = result,
                Count = data.Count()
            };
        }
        public IFactorDto GetLastFactorWithoutComment(Guid userId)
        {
            var factor = _repository.AsQuery().Where(p => p.Customer.UserId == userId &&
                                                          p.ShipmentState == ShipmentState.Delivery)
                .OrderByDescending(p => p.CreationTime).FirstOrDefault();
            if (factor == null) return null;
            var comment = _commentRepository.AsQuery().SingleOrDefault(p => p.FactorId == factor.Id);
            return comment == null ? factor.ToDto() : null;
        }
        public IQueryable<IFactorDto> GetAll()
        {
            var result = _repository.AsQuery().OrderByDescending(item => item.CreationTime)
                .Select(item => new FactorDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    Discount = item.Discount,
                    Price = item.Price,
                    FactorState = item.FactorState,
                    OrderSuggestionId = item.OrderSuggestionId,
                    OrderId = item.OrderId,
                    ShipmentState = item.ShipmentState,
                    DiscountPrice = item.Price * (100 - item.Discount) / 100,
                    FactoRawCount = item.FactorRaws.Count,
                    ShippingTime = item.ShippingTime
                });
            return result;
        }
        public IQueryable<IFactorDto> GetCustomerFactorsByCustomerId(Guid customerId)
        {
            var result = _repository.AsQuery()
                .Where(item => item.Customer.Id == customerId).OrderByDescending(item => item.CreationTime)
                .Select(item => new FactorDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    Discount = item.Discount,
                    Price = item.Price,
                    FactorState = item.FactorState,
                    OrderSuggestionId = item.OrderSuggestionId,
                    OrderId = item.OrderId,
                    ShipmentState = item.ShipmentState,
                    DiscountPrice = item.Price * (100 - item.Discount) / 100,
                    FactoRawCount = item.FactorRaws.Count,
                    ShippingTime = item.ShippingTime
                });
            return result;
        }
        public IQueryable<IFactorDto> GetFactorsByShopId(Guid shopId)
        {
            var result = _repository.AsQuery()
                .Where(item => item.Shop.Id == shopId).OrderByDescending(item => item.CreationTime)
                .Select(item => new FactorDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    Discount = item.Discount,
                    Price = item.Price,
                    FactorState = item.FactorState,
                    OrderSuggestionId = item.OrderSuggestionId,
                    OrderId = item.OrderId,
                    ShipmentState = item.ShipmentState,
                    DiscountPrice = item.Price * (100 - item.Discount) / 100,
                    FactoRawCount = item.FactorRaws.Count,
                    ShippingTime = item.ShippingTime
                });
            return result;
        }
        public IFactorFullInfoDto GetById(long id)
        {
            var factor = _repository.AsQuery().SingleOrDefault(p => p.Id == id);
            if (factor == null)
            {
                throw new DomainException("فاکتور یافت نشد");
            }
            var result = factor.ToDto();
            var factorExpireTime = _applicationSettingRepository.AsQuery().FirstOrDefault()?.FactorExpireTime;
            var factorExpireTimeDate = result.CreationTime.AddMinutes((double)factorExpireTime);
            result.TimeLeft = (int)factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes;
            return result;
        }
        public MobilePagingResultDto<IFactorWithCustomerDto> GetPendingShopFactors(Guid userId, PagedInputDto pagedInput)
        {
            var appSetting = _applicationSettingRepository.AsQuery().FirstOrDefault();
            var factorExpireTime = appSetting?.FactorExpireTime;
            var expireTime = DateTime.Now.AddMinutes(-appSetting.FactorExpireTime);
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var data = _repository.AsQuery()
                .Where(p => p.Shop.Id == shop.Id &&
                            (p.FactorState == FactorState.Pending || p.FactorState == FactorState.Failed) &&
                            p.CreationTime > expireTime);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithCustomerDto())
                .ToList();
            foreach (var factorWithCustomer in result)
            {
                var factorExpireTimeDate = factorWithCustomer.CreationTime.AddMinutes((double)factorExpireTime);
                factorWithCustomer.TimeLeft = (int)factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes;
            }
            return new MobilePagingResultDto<IFactorWithCustomerDto>
            {
                Result = result,
                Count = data.Count()
            };
        }
        public MobilePagingResultDto<IFactorWithCustomerDto> GetPaidShopFactors(Guid userId, PagedInputDto pagedInput)
        {
            var factorExpireTime = _applicationSettingRepository.AsQuery().FirstOrDefault()?.FactorExpireTime;
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var data = _repository.AsQuery()
                .Where(p => p.Shop.Id == shop.Id && p.FactorState == FactorState.Paid);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithCustomerDto())
                .ToList();
            foreach (var factorWithCustomer in result)
            {
                var factorExpireTimeDate = factorWithCustomer.CreationTime.AddMinutes((double)factorExpireTime);
                factorWithCustomer.TimeLeft = (int)factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes;
            }
            return new MobilePagingResultDto<IFactorWithCustomerDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IFactorWithShopDto> GetCustomerFactors(Guid userId, PagedInputDto pagedInput)
        {
            var appSetting = _applicationSettingRepository.AsQuery().FirstOrDefault();
            var factorExpireTime = appSetting?.FactorExpireTime;
            var customer = _customerRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            var expireTime = DateTime.Now.AddMinutes(-appSetting.FactorExpireTime);
            var data = _repository.AsQuery().Where(p => p.Customer.Id == customer.Id
                        && (p.FactorState == FactorState.Paid ||
                        (p.FactorState != FactorState.Paid && p.CreationTime > expireTime)));
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithShopDto())
                .ToList();
            foreach (var factorWithCustomer in result)
            {
                var factorExpireTimeDate = factorWithCustomer.CreationTime.AddMinutes((double)factorExpireTime);
                factorWithCustomer.TimeLeft = (int)factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes;
            }
            return new MobilePagingResultDto<IFactorWithShopDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IFactorWithCustomerDto> GetShopPaidSendFactors(Guid userId, PagedInputDto pagedInput)
        {
            var factorExpireTime = _applicationSettingRepository.AsQuery().FirstOrDefault()?.FactorExpireTime;
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var data = _repository.AsQuery()
                .Where(p => p.Shop.Id == shop.Id && p.FactorState == FactorState.Paid && p.ShipmentState == ShipmentState.Send);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithCustomerDto())
                .ToList();
            foreach (var factorWithCustomer in result)
            {
                var factorExpireTimeDate = factorWithCustomer.CreationTime.AddMinutes((double)factorExpireTime);
                factorWithCustomer.TimeLeft = (int)(factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes < 0 ? 0 : factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes);
            }
            return new MobilePagingResultDto<IFactorWithCustomerDto>
            {
                Count = data.Count(),
                Result = result
            };
        }

        public long GetShopPaidSendFactorsCount(Guid userId)
        {
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var count = _repository.AsQuery()
                .Count(p => p.Shop.Id == shop.Id && p.FactorState == FactorState.Paid && p.ShipmentState == ShipmentState.Send);
            return count;
        }

        public MobilePagingResultDto<IFactorWithCustomerDto> GetShopPaidNotSendFactors(Guid userId, PagedInputDto pagedInput)
        {
            var factorExpireTime = _applicationSettingRepository.AsQuery().FirstOrDefault()?.FactorExpireTime;
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var data = _repository.AsQuery().Where(p =>
                p.Shop.Id == shop.Id && p.FactorState == FactorState.Paid && p.ShipmentState == ShipmentState.Pending);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithCustomerDto())
                .ToList();
            foreach (var factorWithCustomer in result)
            {
                var factorExpireTimeDate = factorWithCustomer.CreationTime.AddMinutes((double)factorExpireTime);
                factorWithCustomer.TimeLeft = (int)(factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes < 0 ? 0 : factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes);
            }
            return new MobilePagingResultDto<IFactorWithCustomerDto>
            {
                Count = data.Count(),
                Result = result
            };
        }

        public long GetShopPaidNotSendFactorsCount(Guid userId)
        {
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var count = _repository.AsQuery().Count(p =>
                p.Shop.Id == shop.Id && p.FactorState == FactorState.Paid && p.ShipmentState == ShipmentState.Pending);
            return count;
        }

        public MobilePagingResultDto<IFactorWithCustomerDto> GetShopPaidDeliveredFactors(Guid userId, PagedInputDto pagedInput)
        {
            var factorExpireTime = _applicationSettingRepository.AsQuery().FirstOrDefault()?.FactorExpireTime;
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var data = _repository.AsQuery().Where(p =>
                p.Shop.Id == shop.Id && p.FactorState == FactorState.Paid && p.ShipmentState == ShipmentState.Delivery);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToFactorWithCustomerDto())
                .ToList();
            foreach (var factorWithCustomer in result)
            {
                var factorExpireTimeDate = factorWithCustomer.CreationTime.AddMinutes((double)factorExpireTime);
                factorWithCustomer.TimeLeft = (int)(factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes < 0 ? 0 : factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes);
            }
            return new MobilePagingResultDto<IFactorWithCustomerDto>
            {
                Count = data.Count(),
                Result = result
            };
        }

        public IQueryable<IFactorReportFinancialDto> GetFactorReportFinancial(DateTime from, DateTime to)
        {
            var result = _repository.AsQuery().Where(p =>
                 p.FactorState == FactorState.Paid
                 && p.CreationTime >= from
                 && p.CreationTime <= to)
                .OrderByDescending(item => item.CreationTime)
                .Select(p => new FactorReportFinancialDto
                {
                    Id = p.Id,
                    CreationTime = p.CreationTime,
                    CustomerFirstName = p.Customer.FirstName,
                    CustomerLastName = p.Customer.LastName,
                    DiscountPrice = p.DiscountPrice,
                    SystemDiscountPrice = p.SystemDiscountPrice,
                    RealDiscountPrice = p.DiscountPrice - p.SystemDiscountPrice,
                    ShopIban = p.Shop.BankAccount.Iban,
                    ShopName = p.Shop.Name

                });

            return result;
        }

        public IFactorTotalReportFinancialDto GetTotalFactorReportFinancial(DateTime from, DateTime to)
        {
            var factors = _repository.AsQuery().Where(p =>
                p.FactorState == FactorState.Paid
                && p.CreationTime >= from
                && p.CreationTime <= to).Select(p => new
                {
                    p.SystemDiscountPrice,
                    p.DiscountPrice,
                    RealDiscountPrice = p.DiscountPrice - p.SystemDiscountPrice
                });
            var result = new FactorTotalReportFinancialDto
            {
                SumDiscountPrice = factors.Sum(p => (decimal?)p.DiscountPrice),
                SumRealDiscount = factors.Sum(p => (decimal?)p.RealDiscountPrice),
                SumSystemDiscount = factors.Sum(p => (decimal?)p.SystemDiscountPrice)
            };
            return result;
        }
    }
}