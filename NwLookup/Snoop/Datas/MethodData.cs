using System;
using System.Reflection;

using NwLookup.Snoop.Collectors;
using NwLookup.Snoop.Views;

namespace NwLookup.Snoop.Datas
{
    public class MethodData : Data
    {
        private MethodInfo Info { get; }

        public MethodData(MethodInfo info, object target)
            : base(info.Name, target) => (Info) = info;

        public override Type Type
            => Info.ReturnType;

        public override string ValueString
            => string.Format("{0}", Type);

        public override string ToString()
            => string.Format("{0}: {1}", Info.Name, ValueString);

        private object Invoke()
        {
            try
            {
                return Info.Invoke(Value, null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void Snoop(ICollector collector)
        {
            if (CanSnoop)
            {
                object value = Invoke();
                if (value != null)
                {
                    Data data = DataFactory.Create(Info, Info.ReturnType, value);
                    SnoopWindow window = new SnoopWindow(collector, data);
                    window.ShowDialog();
                }
            }
        }
    }
}
