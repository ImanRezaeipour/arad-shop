using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Shopping.Infrastructure.Helper
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);
        }
        public static string Total(this IEnumerable<Guid> array)
        {
            return array.OrderBy(t => t).Aggregate(string.Empty, (current, id) => current + id);
        }

        public static DataTable ToDataTableExtension<T>(this IEnumerable<T> items)
        {
            DataTable table = new DataTable();
            PropertyInfo[] properties = typeof(T).GetPublicProperties();
            foreach (var property in properties)
            {
                var description = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(description.Description, property.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(description.Description, property.PropertyType);
            }

            object[] values = new object[properties.Length];
            foreach (var item in items)
            {
                for (int i = 0; i < properties.Length; i++)
                    values[i] = properties[i].GetValue(item);
                table.Rows.Add(values);
            }
            return table;
        }
    }
}