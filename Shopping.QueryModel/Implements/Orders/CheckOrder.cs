using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.Implements.Orders
{
    public class CheckOrder
    {
        public CheckOrder(string title, int timeLeft, CheckOrderStatus status, bool end)
        {
            Title = title;
            TimeLeft = timeLeft;
            Status = status;
            End = end;
        }
        /// <summary>
        /// second
        /// </summary>
        public int TimeLeft { get; set; }
        public CheckOrderStatus Status { get; set; }
        public bool End { get; set; }
        public string Title { get; set; }
    }
}