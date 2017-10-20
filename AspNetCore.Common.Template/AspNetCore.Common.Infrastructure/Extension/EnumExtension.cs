using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AspNetCore.Common.Infrastructure.Extension
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum node) {
            var type = node.GetType();
            var nodeName = Enum.GetName(type, node);
            if (nodeName == null)
                return null;
            var attribute =
                type.GetField(nodeName).GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;
            if (attribute == null)
                return nodeName;
            return attribute.Description;
        }

        public static IEnumerable<EnumMetadata> GetList<T>(int minValue = -99) {
            var type = typeof(T);
            var names = Enum.GetNames(type);
            foreach (var name in names) {
                var value = (int)Enum.Parse(type, name);
                if (value >= minValue)
                    yield return
                        new EnumMetadata {
                            Name = name,
                            Value = value,
                            Description = ((Enum)Enum.Parse(type, name)).GetDescription()
                        };
            }
        }

        public static IEnumerable<EnumMetadata> GetList(Type enumType, int minValue = -99) {
            var names = Enum.GetNames(enumType);
            foreach (var name in names) {
                var value = (int)Enum.Parse(enumType, name);
                if (value >= minValue)
                    yield return
                        new EnumMetadata {
                            Name = name,
                            Value = value,
                            Description = ((Enum)Enum.Parse(enumType, name)).GetDescription()
                        };
            }
        }
    }
}