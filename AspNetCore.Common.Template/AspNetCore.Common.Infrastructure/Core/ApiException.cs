using System;

namespace AspNetCore.Common.Infrastructure.Core
{
    public class ApiException : Exception
    {
        public ApiException(string message)
            : base(message) {
        }
    }
}