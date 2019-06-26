using System.Collections.Generic;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public class DefaultStreamPass : StreamPassBase
    {
        public override bool CanRun(object obj) 
            => true;

        public override void Stream(IList<Data> datas, object obj) 
            => StreamInternal(datas, obj);
    }
}
