using System;

namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public class DateTimeConverter : BaseDataConverter<DateTime, string>
    {
        public override string ConvertTo(DateTime data, object entity) {
            return data.ToString("yyyy-MM-dd HH:mm");
        }
    }
}