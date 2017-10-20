using AspNetCore.Common.Infrastructure.Export.Attributes;
using AspNetCore.Common.Infrastructure.Export.Converter;
using AspNetCore.Common.Infrastructure.Export.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCore.Common.Infrastructure.Export.Enumerators
{
    /// <summary>
    /// 默认导出属性遍历器
    /// </summary>
    public class DefaultExportPropertyEnumerator : IExportPropertyEnumerator
    {
        /// <summary>
        /// 获取导出对象的属性元数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual IEnumerable<ExportProperty> Fetch(object data, IList<IExportFilter> filters) {
            //获取所有的导出属性过滤器
            var propertyFilters = filters == null ? new List<IExportPropertyFilter>() : filters.Where(g => g is IExportPropertyFilter).Select(g => ((IExportPropertyFilter)g));
            var props = GetExportProperties(data);
            var filterProps = new List<ExportProperty>();
            foreach (var prop in props) {
                //执行过滤器,如果filter返回值为true, 则不导出
                if (propertyFilters.Any(g => g.Filter(prop)))
                    continue;

                IDataConvert converter = null;
                if (prop.DataConverter == null)
                    converter = ExportHelper.Converters.Get(prop.ValueType);
                else {
                    converter = Activator.CreateInstance(prop.DataConverter) as IDataConvert;
                }
                if (converter != null)
                    prop.Value = converter.Convert(prop.Value, data);
                filterProps.Add(prop);
            }
            return filterProps.OrderBy(g => g.Order);
        }

        protected virtual IEnumerable<ExportProperty> GetExportProperties(object data) {
            List<ExportProperty> exports = new List<ExportProperty>();
            if (data == null) {
                return exports;
            }
            Type exportType = data.GetType();
            var properties = exportType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties != null) {
                foreach (var property in properties) {
                    ExportAttribute[] exportAttribute = (ExportAttribute[])property.GetCustomAttributes(typeof(ExportAttribute), true);
                    if (exportAttribute.Any() && exportAttribute[0].IsExport) {
                        if (exportAttribute[0].IsRef) {
                            exports.AddRange(GetExportProperties(property.GetValue(data)));
                        }
                        else {
                            exports.Add(new ExportProperty {
                                Value = property.GetValue(data),
                                ValueType = property.PropertyType,
                                FieldName = property.Name,
                                Header = exportAttribute.Any() ? exportAttribute[0].Header : property.Name,
                                Order = exportAttribute.Any() ? exportAttribute[0].Order : 0,
                                DataConverter = exportAttribute.Any() ? exportAttribute[0].DataConverter : null
                            });
                        }
                    }
                }
            }
            return exports;
        }
    }
}