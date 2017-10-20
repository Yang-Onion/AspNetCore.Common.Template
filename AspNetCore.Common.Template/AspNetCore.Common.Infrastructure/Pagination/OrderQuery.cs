using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Infrastructure.Pagination
{
    public class OrderQuery : PageQuery
    {
        public string QueryOrder { get; set; }

        public OrderParams[] GetOrderParams()
        {
            if (!string.IsNullOrWhiteSpace(QueryOrder))
            {
                List<OrderParams> lstParams = new List<OrderParams>();
                var _params = QueryOrder.Split(new char[] { '_' });
                for (var i = 0; i < _params.Length; i += 2)
                {
                    lstParams.Add(new OrderParams()
                    {
                        PropertyName = _params[i],
                    });
                    OrderType type = OrderType.ASC;
                    Enum.TryParse<OrderType>(_params[i + 1], true, out type);
                    lstParams[lstParams.Count - 1].OrderType = type;
                }
                return lstParams.ToArray();
            }
            return null;
        }
    }

    public class OrderParams
    {
        public string PropertyName { get; set; }

        public OrderType OrderType { get; set; }
    }

    public enum OrderType
    {
        ASC,
        DESC
    }
}
