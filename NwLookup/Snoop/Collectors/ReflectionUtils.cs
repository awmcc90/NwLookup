using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NwLookup.Snoop.Collectors
{
    public static class ReflectionUtils
    {
        private static Type[] _types = null;
        public static Type[] Types
        {
            get
            {
                if (_types == null)
                {
                    var baseDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                    _types = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location))
                        .Where(x => Path.GetDirectoryName(x.Location) == baseDirectory)
                        .Where(x => x.GetName().Name.ToLower().Contains("navisworks"))
                        .SelectMany(x => x.GetTypes())
                        .ToArray();
                }
                return _types;
            }
        }

        public static bool IsValidType(Type type, Type objType)
            => objType.IsSubclassOf(type) || objType == type || type.IsAssignableFrom(objType);

        public static IList<Type> GetTypes(object obj)
        {
            if (Marshal.IsComObject(obj))
            {
                string comObjectType = Microsoft.VisualBasic.Information.TypeName(obj);
                return Types.Where(x => x.Name == comObjectType).ToArray();
            }
            else
                return Types.Where(x => IsValidType(x, obj.GetType())).ToArray();
        }

        public static bool IsPrimitive(Type type)
            => type.IsPrimitive || type.IsValueType || type == typeof(string);

        private static BindingFlags ALLOWED_FLAGS 
            => BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

        public static PropertyInfo[] GetPropertyInfo(Type type) 
            => type
                .GetProperties(ALLOWED_FLAGS)
                .Where(x => x.GetMethod != null)
                .OrderBy(x => x.Name)
                .ToArray();

        private static bool IsValidMethod(MethodInfo info) 
            => !(info.IsSpecialName || info.DeclaringType == null || info.GetParameters().Any() || info.ReturnType == typeof(void));

        public static MethodInfo[] GetMethodInfo(Type type) 
            => type
                .GetMethods(ALLOWED_FLAGS)
                .Where(x => IsValidMethod(x))
                .OrderBy(x => x.Name)
                .ToArray();
    }
}
