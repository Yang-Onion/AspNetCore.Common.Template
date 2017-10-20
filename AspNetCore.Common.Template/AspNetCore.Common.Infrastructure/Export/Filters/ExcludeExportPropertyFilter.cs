using AspNetCore.Common.Infrastructure.Export.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Common.Infrastructure.Export.Filters
{
    /// <summary>
    /// 排除导出属性过滤器
    /// </summary>
    public class ExcludeExportPropertyFilter : IExportPropertyFilter
    {
        private readonly IList<string> _excludes;

        public ExcludeExportPropertyFilter() {
            _excludes = new List<string>();
        }

        public ExcludeExportPropertyFilter(params string[] excludes)
            : this() {
            if (excludes != null) {
                foreach (var exclude in excludes) {
                    _excludes.Add(exclude);
                }
            }
        }

        public bool Filter(ExportProperty property) {
            if (_excludes.Count == 0)
                return false;
            return _excludes.Any(g => g.Equals(property.FieldName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}