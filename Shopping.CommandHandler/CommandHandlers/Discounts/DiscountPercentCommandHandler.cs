using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Discounts.Commands;
using Shopping.Commands.Commands.Discounts.Responses;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Entities;
using Shopping.DomainModel.Aggregates.Discounts.Interfaces;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Events;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Core.PersianHelpers;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Discounts
{
    public class DiscountPercentCommandHandler 
        : ICommandHandler<UpdateDiscountPercentCommand, UpdateDiscountPercentCommandResponse>
        , ICommandHandler<CreateDiscountPercentCommand, CreateDiscoutPercentCommandResponse>
        , ICommandHandler<AddProductToPercentDiscountCommand, AddProductToPercentDiscountCommandResponse>
        , ICommandHandler<DeleteProductFromPercentDiscountCommand, DeleteProductFromPercentDiscountCommandResponse>
    {
        private readonly IRepository<PercentDiscount> _percentRepository;
        private readonly IPercentDiscountDomainService _percentDiscountDomainService;
        private readonly IRepository<Product> _productRepository;
        public DiscountPercentCommandHandler(IRepository<PercentDiscount> percentRepository, IPercentDiscountDomainService discountBaseDomainService, IRepository<Product> productRepository)
        {
            _percentRepository = percentRepository;
            _percentDiscountDomainService = discountBaseDomainService;
            _productRepository = productRepository;
        }

        public async Task<CreateDiscoutPercentCommandResponse> Handle(CreateDiscountPercentCommand command)
        {
            await _percentDiscountDomainService.CheckPercentDiscountDate(command.FromDate.ConvertToDate(), command.ToDate.ConvertToDate());
            var userInfo = new UserInfo(command.UserInfoCommand.UserId, command.UserInfoCommand.FirstName,
                command.UserInfoCommand.LastName);

            var discount = new PercentDiscount(Guid.NewGuid(), command.Description, userInfo, command.FromDate.ConvertToDate(),
                command.ToDate.ConvertToDate(), command.Title, command.Percent, command.MaxOrderCount, command.MaxProductCount,
                command.FromTime, command.ToTime)
            {
                ProductDiscounts = new List<ProductDiscount>(),
                Sells = new List<DiscountSell>()
            };
            _percentRepository.Add(discount);
            return new CreateDiscoutPercentCommandResponse();
        }

        public async Task<UpdateDiscountPercentCommandResponse> Handle(UpdateDiscountPercentCommand command)
        {
            var discount = await _percentRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.Id);
            if (discount == null)
            {
                throw new DomainException("تخفیفی یافت نشد");
            }
            _percentDiscountDomainService.ValidationSettingDiscount(command.MaxOrderCount,
                discount.RemainOrderCount, command.MaxProductCount,
                discount.ProductDiscounts.Count);
            discount.FromTime = command.FromTime;
            discount.ToTime = command.ToTime;
            discount.MaxOrderCount = command.MaxOrderCount;
            discount.MaxProductCount = command.MaxProductCount;
            discount.Title = command.Title;
            discount.Description = command.Description;
            DomainEventDispatcher.Raise(new UpdatePercentDiscountEvent(discount.Id, command.Title, command.FromDate.ConvertToDate(), command.ToDate.ConvertToDate()));
            discount.UserInfo = new UserInfo(command.UserInfoCommand.UserId, command.UserInfoCommand.FirstName, command.UserInfoCommand.LastName);
            return new UpdateDiscountPercentCommandResponse();
        }

        public async Task<AddProductToPercentDiscountCommandResponse> Handle(AddProductToPercentDiscountCommand command)
        {
            var product = await _productRepository.AsQuery()
                .SingleOrDefaultAsync(p => p.Id == command.ProductId);
            if (product == null)
            {
                throw new DomainException("محصولی یافت نشد");
            }
            var discount = await _percentRepository.AsQuery()
                .SingleOrDefaultAsync(p => p.Id == command.PercentDiscount);
            if (discount == null)
            {
                throw new DomainException("تخفیف یافت نشد");
            }
            if (discount.ProductDiscounts.Count >= discount.MaxProductCount)
            {
                throw new DomainException("حداکثر تعداد محصول در این تخفیف تکمیل شده است");
            }
            if (discount.ProductDiscounts.Any(p => p.Product.Id == command.ProductId))
            {
                throw new DomainException("این محصول در این تخفیف موجود می باشد");
            }
            var userInfo = new UserInfo(command.UserInfoCommand.UserId, command.UserInfoCommand.FirstName,
                command.UserInfoCommand.LastName);
            var productPercentDiscounts = new ProductDiscount(Guid.NewGuid(), product, userInfo);
            DomainEventDispatcher.Raise(new CreatePercentDiscountEvent(discount.Id, command.ProductId, discount.Title,
                userInfo, discount.FromDate, discount.ToDate,
                discount.Percent, discount.FromTime, discount.ToTime, DiscountType.PercentDiscount));
            discount.ProductDiscounts.Add(productPercentDiscounts);
            return new AddProductToPercentDiscountCommandResponse();
        }

        public async Task<DeleteProductFromPercentDiscountCommandResponse> Handle(DeleteProductFromPercentDiscountCommand command)
        {
            var discount = await _percentRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.PercentDiscount);
            if (discount == null)
            {
                throw new DomainException("تخفیف یافت نشد");
            }
            var productDiscount = discount.ProductDiscounts.SingleOrDefault(p => p.Product.Id == command.ProductId);
            if (productDiscount == null)
            {
                throw new DomainException("محصولی یافت نشد");
            }
            discount.ProductDiscounts.Remove(productDiscount);
            var product = await _productRepository.FindAsync(command.ProductId);
            product.ProductDiscount = null;
            return new DeleteProductFromPercentDiscountCommandResponse();
        }
    }
}