using System;

namespace AspNetCore.Common.Infrastructure.Export.Attributes
{
    public class ExportProperty : ExportAttribute
    {
        public object Value { get; set; }
        public Type ValueType { get; set; }
        public string FieldName { get; set; }
    }
}