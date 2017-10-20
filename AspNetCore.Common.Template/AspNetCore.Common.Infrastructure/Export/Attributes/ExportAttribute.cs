using System;

namespace AspNetCore.Common.Infrastructure.Export.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
            : this(true) {
        }

        public ExportAttribute(bool export) {
            IsExport = export;
        }

        public bool IsExport { get; set; }

        public bool IsRef { get; set; }

        public string Header { get; set; }
        public int Order { get; set; } = 99;

        public Type DataConverter { get; set; }
    }
}