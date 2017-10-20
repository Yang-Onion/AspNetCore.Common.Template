using System.Collections.Generic;
using System;
using AspNetCore.Common.Infrastructure.Export.Enumerators;
using AspNetCore.Common.Infrastructure.Export.Filters;
using AspNetCore.Common.Infrastructure.Export.Attributes;

namespace AspNetCore.Common.Infrastructure.Export.Formaters
{
    public abstract class BaseDataFormater<TResult> : IDataFormater<TResult>
    {
        public BaseDataFormater() {
            Filters = null;
            PropertyEnumerator = new DefaultExportPropertyEnumerator();
        }
        public IList<IExportFilter> Filters { get; set; }

        public IExportPropertyEnumerator PropertyEnumerator { get; set; }

        public TResult Format(IEnumerable<object> data) {
            IList<IEnumerable<ExportProperty>> properties = new List<IEnumerable<ExportProperty>>();
            foreach (var export in data) {
                properties.Add(this.PropertyEnumerator.Fetch(export, this.Filters));
            }
            return Format(properties);
        }

        public abstract TResult Format(IList<IEnumerable<ExportProperty>> entities);

        public abstract TResult Format(object[,] twoDimensArray);
    }
}