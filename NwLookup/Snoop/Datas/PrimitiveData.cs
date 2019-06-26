using System;

namespace NwLookup.Snoop.Datas
{
    public class PrimitiveData : Data
    {
        public PrimitiveData(string label, object type) 
            : base(label, type) { }

        public override bool CanSnoop
            => false;
    }
}
