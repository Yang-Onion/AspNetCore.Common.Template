using System;

namespace AspNetCore.Common.Infrastructure.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message) {
        }
    }
}