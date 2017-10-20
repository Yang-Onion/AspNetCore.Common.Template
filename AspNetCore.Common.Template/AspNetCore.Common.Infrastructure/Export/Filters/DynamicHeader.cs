using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Common.Infrastructure.Export.Attributes;

namespace AspNetCore.Common.Infrastructure.Export.Filters
{
    public class DynamicHeader : IExportPropertyFilter
    {
        private IDictionary<string, string> _dynamicHeaders;

        public DynamicHeader(IDictionary<string, string> dynamicHeaders)
        {
            _dynamicHeaders = dynamicHeaders;
        }

        public bool Filter(ExportProperty property)
        {
            if (_dynamicHeaders != null && _dynamicHeaders.Count > 0)
            {
                foreach (var header in _dynamicHeaders)
                {
                    if (string.Equals(header.Key, property.FieldName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        SetDynamicHader(property, header.Value);
                    }
                }
            }
            return false;
        }

        public void SetDynamicHader(ExportProperty property, string dynamicHeader)
        {
            property.Header = dynamicHeader;
        }
    }
}
