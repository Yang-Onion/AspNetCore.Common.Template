using AspNetCore.Common.Infrastructure.Export.Enumerators;
using AspNetCore.Common.Infrastructure.Export.Filters;
using System.Collections.Generic;

namespace AspNetCore.Common.Infrastructure.Export.Formaters
{
    public interface IDataFormater<TResult>
    {
        IList<IExportFilter> Filters { get; set; }

        IExportPropertyEnumerator PropertyEnumerator { get; set; }

        TResult Format(IEnumerable<object> data);

        TResult Format(object[,] twoDimensArray);
    }
}