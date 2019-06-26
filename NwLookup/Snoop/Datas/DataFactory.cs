using System;
using System.Collections;
using System.Reflection;

using NwLookup.Snoop.Collectors;

namespace NwLookup.Snoop.Datas
{
    public static class DataFactory
    {
        public static Data Create(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                if (ReflectionUtils.IsPrimitive(type))
                    return new PrimitiveData(obj.ToString(), obj);
                // The expected type could be 'object' in the case of the com interface objects
                // in which case we also need to check the actual type of the return value
                // for the pressence of an array
                else if (typeof(IEnumerable).IsAssignableFrom(type) || type.IsArray)
                    return new DataArray(obj.ToString(), obj as IEnumerable);
                else
                    return new DefaultData(obj.ToString(), obj);
            }
            return new DefaultData("null", obj);
        }

        public static Data Create(MemberInfo info, Type expectedType, object returnValue)
        {
            // Strings are technically char arrays so we should check this first befor
            // moving on to checking if the expected type or the returned object type
            // is an array. This is purely a quality of life adjustment
            if (ReflectionUtils.IsPrimitive(expectedType))
                return new PrimitiveData(info.Name, returnValue);
            // The expected type could be 'object' in the case of the com interface objects
            // in which case we also need to check the actual type of the return value
            // for the pressence of an array
            else if (typeof(IEnumerable).IsAssignableFrom(expectedType)
                || (returnValue != null && returnValue.GetType().IsArray))
                return new DataArray(info.Name, returnValue as IEnumerable);
            else
                return new DefaultData(info.Name, returnValue);
        }

        public static Data Create(MethodInfo info, object target) 
            => new MethodData(info, target);

        public static Data Create(MemberInfo info, Type expectedType, Exception e)
            => new ExceptionData(info.Name, e);
    }
}
