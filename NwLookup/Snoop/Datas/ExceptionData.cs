using System;

namespace NwLookup.Snoop.Datas
{
    public class ExceptionData : Data
    {
        public ExceptionData(string label, Exception e) 
            : base(label, e) { }

        public override string ValueString 
            => "Exception";

    }
}
