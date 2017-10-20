using AspNetCore.Common.Infrastructure.Core;

namespace Cms.Infrastructure.Core
{
    public class FinanceException : DomainException
    {
        public decimal Money { get; set; }
        public FinanceException(decimal money, string message) : base(message)
        {
            this.Money = money;
        }
    }
}