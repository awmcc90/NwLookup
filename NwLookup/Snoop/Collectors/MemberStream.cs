using System;
using System.Collections.Generic;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public abstract class MemberStream : IMemberStream
    {
        protected IList<Data> Datas { get; }
        protected object Object { get; }

        public MemberStream(IList<Data> datas, object obj)
            => (Datas, Object) = (datas, obj);

        public void Stream(IList<Type> types)
        {
            foreach (Type type in types)
                Stream(type);
        }

        public abstract void Stream(Type type);
    }
}
