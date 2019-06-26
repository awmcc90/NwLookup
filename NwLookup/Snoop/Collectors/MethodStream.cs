using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public class MethodStream : MemberStream
    {
        public MethodStream(IList<Data> datas, object obj) 
            : base(datas, obj) { }

        [HandleProcessCorruptedStateExceptions]
        public override void Stream(Type type)
        {
            var infos = ReflectionUtils.GetMethodInfo(type);
            foreach (MethodInfo info in infos)
            {
                try
                {
                    //object value = info.Invoke(Object, null);
                    //Datas.Add(DataFactory.Create(info, info.ReturnType, value));
                    Datas.Add(DataFactory.Create(info, Object));
                }
                catch (Exception)
                { }
            }
        }
    }
}
