using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public class ExportConverters : Dictionary<Type, IDataConvert>
    {
        public ExportConverters() {
            this.Add(typeof(DateTime?), new DateTime2Converter());
            this.Add(typeof(DateTime), new DateTimeConverter());
            this.Add(typeof(bool), new BoolConverter());
            this.Add(typeof(Enum), new EnumConverter());
        }

        public IDataConvert Get(Type dataType) {
            if (typeof(Enum).IsAssignableFrom(dataType)) {
                return this[typeof(Enum)];
            }
            if (this.ContainsKey(dataType))
                return this[dataType];
            return null;
        }
    }
}
