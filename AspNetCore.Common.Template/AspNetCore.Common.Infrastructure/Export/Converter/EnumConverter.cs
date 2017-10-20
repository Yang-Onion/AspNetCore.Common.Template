using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCore.Common.Infrastructure.Export.Converter
{
    public class EnumConverter:BaseDataConverter<Enum, string>
    {
        public override string ConvertTo(Enum data, object entity)
        {
            return GetDescription(data);
        }

        private string GetDescription(Enum node)
        {
            var type = node.GetType();
            var nodeName = Enum.GetName(type, node);
            if (nodeName == null)
                return null;
            var attribute =type.GetField(nodeName).GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;
            if (attribute == null)
                return nodeName;
            return attribute.Description;
        }
    }
}
