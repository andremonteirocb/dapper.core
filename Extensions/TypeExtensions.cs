using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Dapper.Core.Extensions
{
    public static class TypeExtensions
    {
        public static string GetTableName(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var tableAttr = typeInfo.GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                if (!string.IsNullOrEmpty(tableAttr.Schema))
                    return $"{tableAttr.Schema}.{tableAttr.Name}";

                return tableAttr.Name;
            }
            var name = type.Name;
            return name;
        }
    }
}
