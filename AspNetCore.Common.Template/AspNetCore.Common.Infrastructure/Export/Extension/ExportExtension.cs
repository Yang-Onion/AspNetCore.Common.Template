using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCore.Common.Infrastructure.Export.Extension
{
    public static class ExportExtension
    {
        public static object GetValue(this object export, string propertyName) {
            Type type = export.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null) {
                throw new Exception(string.Format("该类型没有名为{0}的属性", propertyName));
            }
            var instanceExpression = Expression.Parameter(property.DeclaringType, "instance");
            var memberExpression = Expression.Property(instanceExpression, property);
            var lambdaExpression = Expression.Lambda(memberExpression, instanceExpression);
            return lambdaExpression.Compile().DynamicInvoke(export);
        }
    }
}