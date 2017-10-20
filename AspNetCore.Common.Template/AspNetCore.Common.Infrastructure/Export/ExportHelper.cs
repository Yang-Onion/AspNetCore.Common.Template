using AspNetCore.Common.Infrastructure.Export.Converter;
using AspNetCore.Common.Infrastructure.Export.Filters;
using AspNetCore.Common.Infrastructure.Export.Formaters;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCore.Common.Infrastructure.Export
{
    public static class ExportHelper
    {
        public static ExportConverters Converters { get; set; } = new ExportConverters();

        /// <summary>
        /// 导出数据，返回T类型的容器
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="formater">数据格式化处理类</param>
        /// <returns></returns>
        public static TResult Export<TResult>(IEnumerable<object> data, IDataFormater<TResult> formater)
        {
            return formater.Format(data);
        }

        public static Stream ExportExcel(IEnumerable<object> data, params IExportFilter[] filters)
        {
            return new ExcelFormater() { Filters = filters }.Format(data);
        }

        public static Stream ExportExcel(object[,] towDimensArray)
        {
            return new ExcelFormater().Format(towDimensArray);
        }
    }
}