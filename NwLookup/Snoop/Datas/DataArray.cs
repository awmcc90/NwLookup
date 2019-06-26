using System;
using System.Collections;
using System.Collections.Generic;

namespace NwLookup.Snoop.Datas
{
    public class DataArray : Data
    {
        public List<object> Array { get; } = new List<object>();

        public Type InnerType 
            => CanSnoop ? Array[0].GetType() : null;

        public int Length 
            => Array.Count;

        public DataArray(string label, IEnumerable value) 
            : base(label, value)
        {
            IEnumerator iter = value.GetEnumerator();
            while (iter.MoveNext())
                Array.Add(iter.Current);
        }

        public override string ValueString 
            => string.Format("{0}[{1}]", InnerType, Length);

        public override bool CanSnoop
            => base.CanSnoop && Length > 0;

    }
}
