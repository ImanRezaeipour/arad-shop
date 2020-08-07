using System;
using Shopping.Infrastructure.Core.Enums;

namespace Shopping.Infrastructure.Core
{
    public class DomainException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }

        public DomainException(string message, ErrorCode errorCode = ErrorCode.Default) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}