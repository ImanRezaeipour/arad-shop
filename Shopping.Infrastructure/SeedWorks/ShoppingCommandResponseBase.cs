using System;
using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.Infrastructure.SeedWorks
{
    public abstract class ShoppingCommandResponseBase : ICommandResponse
    {
        protected ShoppingCommandResponseBase()
        {
            ResponseTime = DateTime.Now;
        }
        /// <summary>
        /// زمان پاسخ
        /// </summary>
        public DateTime ResponseTime { get; private set; }
    }
}