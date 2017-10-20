using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public class BoolConverter : BaseDataConverter<bool, string>
    {
        public override string ConvertTo(bool data, object entity) {
            return data ? "是" : "否";
        }
    }
}
