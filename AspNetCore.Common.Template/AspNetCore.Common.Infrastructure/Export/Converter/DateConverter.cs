using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public class DateConverter : BaseDataConverter<DateTime, string>
    {
        public override string ConvertTo(DateTime data, object entity) {
            return data.ToString("yyyy-MM-dd");
        }
    }
}
