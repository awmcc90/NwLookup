using System;
using System.Collections.Generic;

namespace NwLookup.Snoop.Collectors
{
    public interface IMemberStream
    {
        void Stream(IList<Type> types);
        void Stream(Type type);
    }
}
