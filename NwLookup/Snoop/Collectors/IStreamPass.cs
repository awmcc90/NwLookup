using System;
using System.Collections.Generic;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public interface IStreamPass
    {
        bool CanRun(object obj);
        void Stream(IList<Data> datas, object obj);
    }
}
