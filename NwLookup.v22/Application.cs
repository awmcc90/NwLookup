using System;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using NwLookup.Snoop.Datas;
using NwLookup.v22.Snoop.Collectors;
using ComApi = Autodesk.Navisworks.Api.Interop.ComApi;

namespace NwLookup.v22
{
    [Plugin("NwLookup.v22.Application", "AMCC", DisplayName = "Snoop")]
    [Strings("NwLookupDefinitions.txt")]
    [RibbonLayout("NwLookupLayout.xaml")]
    [RibbonTab("ID_NWLOOKUP_TAB")]
    [Command("NWLOOKUP_SNOOP_COMMAND",
        Icon = "resources\\mg_16x16.ico",
        LargeIcon = "resources\\mg_32x32.ico",
        CallCanExecute = CallCanExecute.Always,
        CanToggle = true,
        LoadForCanExecute = true)]
    public class Application : CommandHandlerPlugin
    {
        public override int ExecuteCommand(string name, params string[] parameters)
        {
            switch (name)
            {
                case "NWLOOKUP_SNOOP_COMMAND":
                    NwLookupCommand();
                    break;
                default:
                    break;
            }
            
            return 0;
        }

        public override CommandState CanExecuteCommand(string name)
        {
            switch (name)
            {
                case "NWLOOKUP_SNOOP_COMMAND":
                    return new CommandState(true);
                default:
                    throw new NotImplementedException(
                        string.Format("Command with name {0} is not implemented", name)
                        );
            }
        }

        private void NwLookupCommand()
        {
            // Force loading of the Interop.ComApi assembly so that it will be included when
            // the 'Types' call is made in reflection utils
            GC.KeepAlive(typeof(ComApi.InwBase));
            Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            Data data;
            if (document.CurrentSelection.SelectedItems.IsEmpty)
                data = DataFactory.Create(document);
            else
                data = DataFactory.Create(document.CurrentSelection.SelectedItems);
            data.Snoop(new CustomCollector());
        }
    }
}
