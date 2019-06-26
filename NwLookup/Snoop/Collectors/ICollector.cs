using System.Collections.Generic;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public interface ICollector
    {
        void Stream(IList<Data> datas, object obj);
    }
}
