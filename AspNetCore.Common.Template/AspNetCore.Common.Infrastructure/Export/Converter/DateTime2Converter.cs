using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public class DateTime2Converter : BaseDataConverter<DateTime?, string>
    {
        public override string ConvertTo(DateTime? data, object entity) {
            return data == null ? string.Empty : data.Value.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
