using System.Collections.Generic;
using Autodesk.Navisworks.Api;
using NwLookup.Snoop.Collectors;
using NwLookup.Snoop.Datas;
using ComApi = Autodesk.Navisworks.Api.Interop.ComApi;
using ComBridge = Autodesk.Navisworks.Api.ComApi.ComApiBridge;

namespace NwLookup.v22.Snoop.Collectors
{
    public class ModelItemStreamPass : StreamPassBase
    {
        public override bool CanRun(object obj)
            => obj is ModelItem;

        public override void Stream(IList<Data> datas, object obj)
        {
            ComApi.InwOaPath path = ComBridge.ToInwOaPath((ModelItem)obj);
            StreamInternal(datas, path);
        }
    }
}
