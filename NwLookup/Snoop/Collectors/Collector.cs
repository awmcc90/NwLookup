using System;
using System.Collections.Generic;

using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Collectors
{
    public class Collector : ICollector
    {
        private IList<IStreamPass> Passes { get; }

        public Collector(IList<IStreamPass> passes) 
            => Passes = passes;

        public Collector()
            : this(new List<IStreamPass>()) { }

        public void AddPass(IStreamPass pass) 
            => Passes.Add(pass);

        public void Stream(IList<Data> datas, object obj)
        {
            foreach (IStreamPass pass in Passes)
                if (pass.CanRun(obj))
                    pass.Stream(datas, obj);
        }
    }
}
