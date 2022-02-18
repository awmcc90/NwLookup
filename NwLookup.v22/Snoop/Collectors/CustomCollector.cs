using System.Collections.Generic;
using NwLookup.Snoop.Collectors;

namespace NwLookup.v22.Snoop.Collectors
{
    public class CustomCollector : Collector
    {
        public CustomCollector() 
            : base(new List<IStreamPass>{
                new DefaultStreamPass(),
                new ModelItemStreamPass()
            }) { }
    }
}
