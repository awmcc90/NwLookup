using System;
using System.Collections.Generic;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public abstract class StreamPassBase : IStreamPass
    {
        public abstract bool CanRun(object obj);

        public abstract void Stream(IList<Data> datas, object obj);

        protected void StreamInternal(IList<Data> datas, object obj)
        {
            IList<Type> types = ReflectionUtils.GetTypes(obj);

            if (types.Count == 0)
                return;

            IMemberStream[] streams = new IMemberStream[]
            {
                new PropertyStream(datas, obj),
                new MethodStream(datas, obj)
            };
            foreach (IMemberStream stream in streams)
                stream.Stream(types);
        }
    }
}
