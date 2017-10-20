using AspNetCore.Common.Infrastructure.Export.Attributes;
namespace AspNetCore.Common.Infrastructure.Export.Filters
{
    /// <summary>
    /// 导出属性过滤器
    /// </summary>
    public interface IExportPropertyFilter : IExportFilter
    {
        /// <summary>
        /// 过滤导出属性
        /// </summary>
        /// <param name="meta"></param>
        /// <returns></returns>
        bool Filter(ExportProperty property);
    }
}