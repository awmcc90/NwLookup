using System;

using NwLookup.Snoop.Collectors;
using NwLookup.Snoop.Views;

namespace NwLookup.Snoop.Datas
{
    public abstract class Data
    {
        public string Label { get; }

        public object Value { get; }

        public Data(string label, object value)
            => (Label, Value) = (label, value);

        public virtual Type Type
            => Value?.GetType();

        public virtual string ValueString
            => Value == null ? "null" : Value.ToString();

        public virtual bool CanSnoop
            => Value != null;

        public override string ToString() 
            => string.Format("{0}: {1}", Label, ValueString);

        public virtual void Snoop(ICollector collector)
        {
            if (CanSnoop)
            {
                SnoopWindow window = new SnoopWindow(collector, this);
                window.ShowDialog();
            }
        }

    }
}
