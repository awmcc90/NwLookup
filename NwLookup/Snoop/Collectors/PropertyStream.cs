using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public class PropertyStream : MemberStream
    {
        public PropertyStream(IList<Data> datas, object obj) 
            : base(datas, obj) { }

        [HandleProcessCorruptedStateExceptions]
        public override void Stream(Type type)
        {
            var infos = ReflectionUtils.GetPropertyInfo(type);
            foreach (PropertyInfo info in infos)
            {
                try
                {
                    object value = info.GetValue(Object);
                    Datas.Add(DataFactory.Create(info, info.PropertyType, value));
                }
                catch (Exception)
                { }
            }
        }
    }
}
