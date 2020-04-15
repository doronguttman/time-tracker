using System;

namespace Extensions
{
    public static class TypeExtensions
    {
        public static string GetShortName(this Type type)
        {
            var parts = (type.FullName ?? type.Name).Split('.');
            for (var i = 0; i < parts.Length - 1; i++) parts[i] = parts[i][0].ToString();
            return string.Join(".", parts);
        }

        public static object CreateInstance(this Type type) => Activator.CreateInstance(type);
    }
}
