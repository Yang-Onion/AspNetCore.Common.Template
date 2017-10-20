using AspNetCore.Common.Infrastructure.Export.Attributes;
using AspNetCore.Common.Infrastructure.Export.Filters;
using System.Collections.Generic;

namespace AspNetCore.Common.Infrastructure.Export.Enumerators
{
    /// <summary>
    /// 导出属性遍历接口
    /// </summary>
    public interface IExportPropertyEnumerator
    {
        IEnumerable<ExportProperty> Fetch(object data, IList<IExportFilter> propertyFilters);
    }
}