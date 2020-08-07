namespace Shopping.QueryService.Implements.SeedWorks
{
    public static class PriceConvertor
    {
        public static decimal ConvertToToman(this decimal price)
        {
            return decimal.Floor(price / 10);
        }
    }
}