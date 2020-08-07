using System.ComponentModel;

namespace Shopping.Payment.Models
{
    public enum ParsianErrorCode
    {
        [Description("خطاي ناشناخته رخ داده است")]
        UnkownError= -32768,
        [Description("برگشت تراکنش مجاز نمی باشد")]
        PaymentRequestIsNotEligibleToReversal = -1552,
        [Description("برگشت تراکنش قبلا اًنجام شده است")]
        PaymentRequestIsAlreadyReversed = -1551,
        [Description("برگشت تراکنش در وضعیت جاري امکان پذیر نمی باشد")]
        PaymentRequestStatusIsNotReversalable = -1550,
        [Description("زمان مجاز براي درخواست برگشت تراکنش به اتمام رسیده است")]
        MaxAllowedTimeToReversalHasExceeded = -1549,
        [Description("فراخوانی سرویس درخواست پرداخت قبض ناموفق بود")]
        BillPaymentRequestServiceFailed = -1548,
        [Description("تایید تراکنش ناموفق می باشد")]
        InvalidConfirmRequestService = -1540,
        [Description("فراخوانی سرویس درخواست شارژ تاپ آپ ناموفق بود")]
        TopupChargeServiceTopupChargeRequestFailed = -1536,
        [Description("تراکنش قبلاً تایید شده است")]
        PaymentIsAlreadyConfirmed = -1533,
        [Description("تراکنش از سوي پذیرنده تایید شد")]
        MerchantHasConfirmedPaymentRequest = -1532,
        [Description("تایید تراکنش ناموفق امکان پذیر نمی باشد")]
        CannotConfirmNonSuccessfulPayment = -1531,
        [Description("ده مجاز به تایید این تراکنش نمی باشد")]
        MerchantConfirmPaymentRequestAccessVaiolated = -1530,
        [Description("اطلاعات پرداخت یافت نشد")]
        ConfirmPaymentRequestInfoNotFound = -1528,
        [Description("انجام عملیات درخواست پرداخت تراکنش خرید ناموفق بود")]
        CallSalePaymentRequestServiceFailed = -1527,
        [Description("تراکنش برگشت به سوئیچ ارسال شد")]
        ReversalCompleted = -1507,
        [Description("تایید تراکنش توسط پذیرنده انجام شد")]
        PaymentConfirmRequested = -1505,
        [Description("عملیات پرداخت توسط کاربر لغو شد")]
        CanceledByUser = -138,
        [Description("مبلغ تراکنش کمتر از حداقل مجاز میباشد")]
        InvalidMinimumPaymentAmount = -132,
        [Description("نامعتبر می باشد Token")]
        InvalidToken=-131,
        [Description("زمان منقضی شده است Token")]
        TokenIsExpired= -130,
        [Description("قالب آدرس ip معتبر نمی باشد")]
        InvalidIpAddressFormat = -128,
        [Description("آدرس اینترنتی معتبر نمی باشد")]
        InvalidMerchantIp = -127,
        [Description("کد شناسایی پذیرنده معتبر نمی باشد")]
        InvalidMerchantPin = 126,
        [Description("رشته داده شده بطور کامل عددي نمی باشد")]
        InvalidStringIsNumeric = -121,
        [Description("طول داده ورودي معتبر نمی باشد")]
        InvalidLength= 120,
        [Description("سازمان نامعتبر می باشد")]
        InvalidOrganizationId = -119,
        [Description("مقدار ارسال شده عدد نمی باشد")]
        ValueIsNotNumeric = -118,
        [Description("طول رشته کم تر از حد مجاز می باشد")]
        LenghtIsLessOfMinimum = -117,
        [Description("طول رشته بیش از حد مجاز می باشد")]
        LenghtIsMoreOfMaximum = -116,
        [Description("شناسه پرداخت نامعتبر می باشد")]
        InvalidPayId=-115,
        [Description("شناسه قبض نامعتبر می باشد")]
        InvalidBillId =-114,
        [Description("پارامتر ورودي خالی می باشد")]
        ValueIsNull=-113,
        [Description("شماره سفارش تکراري است")]
        OrderIdDuplicated = -112,
        [Description("مبلغ تراکنش بیش از حد مجاز پذیرنده می باشد")]
        InvalidMerchantMaxTransAmount = -111,
        [Description("قابلیت برگشت تراکنش براي پذیرنده غیر فعال می باشد")]
        ReverseIsNotEnabled = -108,
        [Description("قابلیت ارسال تاییده تراکنش براي پذیرنده غیرفعال می باشد")]
        AdviceIsNotEnabled = -107,
        [Description("قابلیت شارژ براي پذیرنده غیر فعال می باشد")]
        ChargeIsNotEnabled = -106,
        [Description("قابلیت تاپ آپ براي پذیرنده غیر فعال می باشد")]
        TopupIsNotEnabled = -105,
        [Description("قابلیت پرداخت قبض براي پذیرنده غیر فعال می باشد")]
        BillIsNotEnabled=-104,
        [Description("قابلیت خرید براي پذیرنده غیر فعال می باشد")]
        SaleIsNotEnabled=-103,
        [Description("تراکنش با موفقیت برگشت داده شد")]
        ReverseSuccessful= -102,
        [Description("پذیرنده اهراز هویت نشد")]
        MerchantAuthenticationFailed = -101,
        [Description("پذیرنده غیرفعال می باشد")]
        MerchantIsNotActive = -100,
        [Description("خطاي سرور")]
        ServerError=-1,
        [Description("عملیات موفق می باشد")]
        Successful =0,
        [Description("صادرکننده ي کارت از انجام تراکنش صرف نظر کرد")]
        ReferToCardIssuerDecline = 1,
        [Description("عملیات تاییدیه این تراکنش قبلا باموفقیت صورت پذیرفته است")]
        ReferToCardIssuerSpecialConditions = 2,
        [Description("پذیرنده ي فروشگاهی نامعتبر می باشد")]
        InvalidMerchant = 3,
        [Description("از انجام تراکنش صرف نظر شد")]
        DoNotHonour=5,
        [Description("بروز خطایی ناشناخته")]
        Error =6,
        [Description("باتشخیص هویت دارنده ي کارت، تراکنش موفق می یاشد")]
        HonourWithIdentification = 8,
        [Description("درخواست رسیده در حال پی گیري و انجام است")]
        RequestInprogress = 9,
        [Description("تراکنش با مبلغی پایین تر از مبلغ درخواستی کمبود حساب مشتری پذیرفته شده است")]
        ApprovedForPartialAmount = 10,
        [Description("تراکنش نامعتبر است")]
        InvalidTransaction = 12,
        [Description("مبلغ تراکنش نادرست است")]
        InvalidAmount = 13,
        [Description("")]
        InvalidCardNumber = 14,
        [Description("")]
        NoSuchIssuer = 15,
        [Description("")]
        CustomerCancellation = 17,
        [Description("")]
        InvalidResponse = 20,
        [Description("")]
        NoActionTaken = 21,
        [Description("")]
        SuspectedMalfunction = 22,
        [Description("")]
        FormatError = 30,
        [Description("")]
        BankNotSupportedBySwitch = 31,
        [Description("")]
        CompletedPartially = 32,
        [Description("")]
        ExpiredCardPickUp = 33,
        [Description("")]
        AllowablePINTriesExceededPickUp = 38,
        [Description("")]
        NoCreditAcount = 39,
        [Description("")]
        RequestedFunctionisnotsupported = 40,
        [Description("")]
        LostCard= 41,
        [Description("")]
        StolenCard = 43,
        [Description("")]
        BillCannotBePayed = 45,
        [Description("")]
        NoSufficientFunds = 51,
        [Description("")]
        ExpiredAccount = 54,
        [Description("")]
        IncorrectPIN = 55,
        [Description("")]
        NoCardRecord = 56,
        [Description("")]
        TransactionNotPermittedToCardHolder = 57,
        [Description("")]
        TransactionNotPermittedToTerminal = 58,
        [Description("")]
        SuspectedFraudDecline = 59,
        [Description("")]
        ExceedsWithdrawalAmountLimit = 61,
        [Description("")]
        RestrictedCardDecline = 62,
        [Description("")]
        SecurityViolation = 63,
        [Description("")]
        ExceedsWithdrawalFrequencyLimit = 65,
        [Description("")]
        ResponseReceivedTooLate = 68,
        [Description("")]
        AllowabeNumberOfPINTriesExceeded = 69,
        [Description("")]
        PINRetiesExceedsSlm = 75,
        [Description("")]
        DeactivatedCardSlm = 78,
        [Description("")]
        InvalidAmountSlm = 79,
        [Description("")]
        TransactionDeniedSlm = 80,
        [Description("")]
        CancelledCardSlm = 81,
        [Description("")]
        HostRefuseSlm = 83,
        [Description("")]
        IssuerDownSlm = 84,
        [Description("")]
        IssuerOrSwitchIsInoperative = 91,
        [Description("")]
        FinancialInstOrIntermediateNetFacilityNotFoundforRouting = 92,
        [Description("")]
        TranactionCannotBeCompleted = 93,

    }
}