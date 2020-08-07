using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kernel.Notify.Message.Interfaces;
using Parbad.Core;
using Parbad.Mvc5;
using Parbad.Providers;
using Shopping.DomainModel.Aggregates.Discounts.Interfaces;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Interfaces;
using Shopping.DomainModel.Aggregates.Payments.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Interfaces;
using Shopping.DomainModel.Aggregates.Promoters.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Payment.ParsianGateway;
using Shopping.Payment.ParsianGatewayConfirm;
using Shopping.Payment.ParsianGatewayReverse;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.Payment.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IContext _context;
        private readonly IRepository<DomainModel.Aggregates.Payments.Aggregates.Payment> _paymentRepository;
        private readonly IRepository<Factor> _factorRepository;
        private readonly IFcmNotification _fcmNotification;
        private const string LoginAccount = "6OD88e775JyEf5vE71aI";
        private readonly IPercentDiscountDomainService _percentDiscountDomainService;
        private readonly IFactorDomainService _factorDomainService;
        private readonly IPersonDomainService _personDomainService;
        private readonly IPromoterDomainService _promoterDomainService;
        public PaymentController(
            IRepository<DomainModel.Aggregates.Payments.Aggregates.Payment> paymentRepository,
            IContext context,
            IRepository<Factor> factorRepository,
            IFcmNotification fcmNotification, IPercentDiscountDomainService percentDiscountDomainService, IFactorDomainService factorDomainService, IPersonDomainService personDomainService, IPromoterDomainService promoterDomainService)
        {
            _paymentRepository = paymentRepository;
            _context = context;
            _factorRepository = factorRepository;
            _fcmNotification = fcmNotification;
            _percentDiscountDomainService = percentDiscountDomainService;
            _factorDomainService = factorDomainService;
            _personDomainService = personDomainService;
            _promoterDomainService = promoterDomainService;
        }
        [HttpGet]
        public ActionResult Pay(long paymentId, string callbackUrl)
        {
#if DEBUG

            var payment = _paymentRepository.Find(paymentId);
            if (payment == null)
            {
                //show error page
                @ViewBag.hasError = true;
                @ViewBag.errorMessage = "فاکتور مورد نظر یافت نشد";
                return View();
            }
            var factor = _factorRepository.Find(payment.FactorId);
            @ViewBag.factorId = factor.Id;
            if (factor.HaveFactorRawPercentDiscount())
            {
                if (_factorDomainService.HavePercentDiscountToday(factor.Customer))
                {
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "شما مجاز به انتخاب یک کالای دارای تخفیف درصدی در هر روز می باشید";
                    return View();
                }
            }


            var invoice = new Invoice(paymentId, payment.Amount, callbackUrl);
            var result = Parbad.Payment.Request(Gateway.ParbadVirtualGateway, invoice);
            switch (result.Status)
            {
                case RequestResultStatus.Success:
                    // کاربر را به سمت وب سایت درگاه پرداخت هدایت میکند
                    return new RequestActionResult(result);
                case RequestResultStatus.DuplicateOrderNumber:
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "کد تراکنش تکراری می باشد";
                    break;
                case RequestResultStatus.Failed:
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "عدم ارتباط با پذیرنده";
                    break;
                default:
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "عدم ارتباط با پذیرنده";
                    break;
            }
            @ViewBag.result = result;

            return View();
#else
            try
            {
                var payment = _paymentRepository.Find(paymentId);
                if (payment == null)
                {
                    //show error page
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "فاکتور مورد نظر یافت نشد";
                    return View();
                }
                var factor = _factorRepository.Find(payment.FactorId);
                @ViewBag.factorId = factor.Id;

                var service = new SaleServiceSoapClient();
                var response = service.SalePaymentRequest(new ClientSaleRequestData
                {
                    Amount = payment.Amount,
                    CallBackUrl = callbackUrl,
                    OrderId = paymentId,
                    LoginAccount = LoginAccount
                });
                if (response.Status == 0)
                {
                    // کاربر را به سمت وب سایت درگاه پرداخت هدایت میکند
                    var ipgUrl = ConfigurationManager.AppSettings["ParsianIpgURL"];
                    return Redirect(ipgUrl + response.Token);
                }
                @ViewBag.hasError = true;
                @ViewBag.errorMessage = response.Message;
                return View();
            }
            catch (Exception)
            {
                @ViewBag.hasError = true;
                @ViewBag.errorMessage = "خطای داخلی سرور";
                return View();
            }
#endif
        }

        //test اینکه دستی خودم اینو کال کنم چیه
        public async Task<ActionResult> Verify(long paymentIdentity)
        {
#if DEBUG
            var payment = _paymentRepository.Find(paymentIdentity);
            if (payment == null)
            {
                @ViewBag.hasError = true;
                @ViewBag.errorMessage = "فاکتور مورد نظر یافت نشد";
                return View();
            }
            var factor = _factorRepository.Find(payment.FactorId);
            if (factor == null)
            {
                @ViewBag.hasError = true;
                @ViewBag.errorMessage = "فاکتور مورد نظر یافت نشد";
                return View();
            }
            var result = await Parbad.Payment.VerifyAsync(System.Web.HttpContext.Current);
            if (result.Status == VerifyResultStatus.Success)
            {
                @ViewBag.hasError = false;
                @ViewBag.successMessage = "عملیات با موفقیت صورت پذیرفت";

                factor.PayFactor(Guid.NewGuid(), result.ReferenceId, result.TransactionId, result.Status, result.Message);
                payment.PaymentType = PaymentType.Paid;

                _percentDiscountDomainService.AddDiscountSellToPercentDiscount(factor);
                _personDomainService.ShopCustomerSubsetPaidFactor(factor.Shop, factor.Customer);
                _promoterDomainService.PromoterCustomerSubsetPaidFactor(factor.Customer);

                await _fcmNotification.SendToIds(factor.Shop.GetPushTokens(), "Successfull payment",
                    $"Your payment with {factor.Id} has been successfully executed", NotificationType.FactorPayed,
                    AppType.Shop, NotificationSound.Shopper);
            }
            else
            {
                @ViewBag.hasError = true;
                @ViewBag.errorMessage = "خطا در انجام عملیات";
                @ViewBag.errorMesaageDescription = result.Message;
                factor.FailedFactor(Guid.NewGuid(), result.ReferenceId, result.TransactionId, result.Status, result.Message);
                payment.PaymentType = PaymentType.Failed;
            }
            payment.PaymentResponse = new PaymentResponse(result.ReferenceId, result.TransactionId, result.Message,
                result.Status);
            _context.SaveChanges();
            @ViewBag.factorId = factor.Id;
            return View();
#else
            var tokenString = Request.Form["Token"];
            var statusString = Request.Form["status"];
            var orderIdString = Request.Form["OrderId"];
            var rrn = Request.Form["RRN"];
            if (!string.IsNullOrEmpty(tokenString) && !string.IsNullOrEmpty(statusString) &&
                !string.IsNullOrEmpty(orderIdString) && !string.IsNullOrEmpty(rrn))
            {
                var status = int.Parse(statusString);
                var orderId = long.Parse(orderIdString);
                var token = long.Parse(tokenString);
                var payment = _paymentRepository.Find(orderId);
                if (payment == null)
                {
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "فاکتور مورد نظر یافت نشد";
                    return View();
                }
                var factor = _factorRepository.Find(payment.FactorId);
                if (factor == null)
                {
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "فاکتور مورد نظر یافت نشد";
                    return View();
                }
                try
                {
                    if (status == 0 && token > 0)
                    {
                        var service = new ConfirmServiceSoapClient();
                        var response = service.ConfirmPayment(new ClientConfirmRequestData
                        {
                            Token = token,
                            LoginAccount = LoginAccount
                        });
                        bool hasError = false;
                        if (response.Status == 0 && response.RRN > 0)
                        {
                            @ViewBag.hasError = false;
                            @ViewBag.successMessage = "عملیات با موفقیت صورت پذیرفت";

                            factor.PayFactor(Guid.NewGuid(), rrn, tokenString, VerifyResultStatus.Success, "موفق");
                            payment.PaymentType = PaymentType.Paid;

                            _percentDiscountDomainService.AddDiscountSellToPercentDiscount(factor);
                            _personDomainService.ShopCustomerSubsetPaidFactor(factor.Shop, factor.Customer);
                            _promoterDomainService.PromoterCustomerSubsetPaidFactor(factor.Customer);

                            await _fcmNotification.SendToIds(factor.Shop.GetPushTokens(), "پرداخت فاکتور موفق",
                                $"پرداخت فاکتور با شماره {factor.Id} انجام شد", NotificationType.FactorPayed,
                                AppType.Shop, NotificationSound.Shopper);
                        }
                        else
                        {
                            hasError = true;
                            @ViewBag.hasError = true;
                            @ViewBag.errorMessage = "خطا در انجام عملیات";
                            factor.FailedFactor(Guid.NewGuid(), rrn, tokenString, VerifyResultStatus.Failed, "ناموفق");
                            payment.PaymentType = PaymentType.Failed;
                        }

                        payment.PaymentResponse = new PaymentResponse(rrn, tokenString, "",
                            hasError ? VerifyResultStatus.Failed : VerifyResultStatus.Success);
                        _context.SaveChanges();
                        @ViewBag.factorId = factor.Id;
                        return View();
                    }
                }
                catch (Exception e)
                {
                    var service = new ReversalServiceSoapClient();
                    service.ReversalRequest(new ClientReversalRequestData
                    {
                        LoginAccount = LoginAccount,
                        Token = token,
                    });
                    factor.FailedFactor(Guid.NewGuid(), rrn, tokenString, VerifyResultStatus.Failed, "ناموفق");
                    payment.PaymentType = PaymentType.Reverse;
                    payment.PaymentResponse = new PaymentResponse(rrn, tokenString, "", VerifyResultStatus.Failed);
                    @ViewBag.hasError = true;
                    @ViewBag.errorMessage = "در صورت کسر وجه از حساب شما مبلغ مذکور طی 72 ساعت به حساب شما عودت داده خواهد شد. در غیر این صورت جهت پیگیری لطفا با پشتیبانی تماس حاصل فرمایید.";
                    return View();
                }
            }
            @ViewBag.hasError = true;
            @ViewBag.errorMessage = "ارتباط با درگاه آنلاین برقرار نشد";
            return View();
#endif
        }
    }
}