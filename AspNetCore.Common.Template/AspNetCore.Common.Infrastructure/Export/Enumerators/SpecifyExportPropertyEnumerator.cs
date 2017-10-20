using AspNetCore.Common.Infrastructure.Export.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Common.Infrastructure.Export.Enumerators
{
    /// <summary>
    /// 指定导出属性遍历器
    /// </summary>
    public class SpecifyExportPropertyEnumerator : DefaultExportPropertyEnumerator
    {
        private readonly List<string> _propertyNames = new List<string>();

        /// <summary>
        /// 按照参数顺序导出指定列,该列必须设置ExportAttribute特性
        /// </summary>
        /// <param name="orderedProperties"></param>
        public SpecifyExportPropertyEnumerator(params string[] orderedPropertieNames) {
            if (orderedPropertieNames != null)
                _propertyNames.AddRange(orderedPropertieNames);
        }

        protected override IEnumerable<ExportProperty> GetExportProperties(object data) {
            var properties = base.GetExportProperties(data);
            var orderedProperties = new List<ExportProperty>();
            ExportProperty matchProperty = null;
            var index = 0;
            foreach (var propertyName in _propertyNames) {
                matchProperty = properties.FirstOrDefault(g => string.Equals(g.FieldName, propertyName, StringComparison.CurrentCultureIgnoreCase));
                if (matchProperty != null) {
                    matchProperty.Order = index;
                    orderedProperties.Add(matchProperty);
                    index++;
                }
            }
            return orderedProperties;
        }
    }
}