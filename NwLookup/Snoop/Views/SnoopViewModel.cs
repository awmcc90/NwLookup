using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using NwLookup.Snoop.Datas;
using NwLookup.Snoop.Collectors;
using NwLookup.Snoop.Views.Models;

namespace NwLookup.Snoop.Views
{
    public class SnoopViewModel : NotifyViewModelBase
    {
        private ObservableCollection<object> _objects = null;
        public ObservableCollection<object> Objects
        {
            get { return _objects; }
            set
            {
                if (_objects != value)
                {
                    _objects = value;
                    OnPropertyChanged();
                    if (_objects != null && _objects.Count > 0)
                        SelectedObject = _objects[0];
                }
            }
        }

        private object _selectedObject = null;
        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (_selectedObject != value)
                {
                    _selectedObject = value;
                    OnPropertyChanged();
                    Snoop(_selectedObject);
                }
            }
        }

        public ObservableCollection<Data> Datas { get; } = new ObservableCollection<Data>();

        private Data _selectedData = null;
        public Data SelectedData
        {
            get { return _selectedData; }
            set
            {
                if (_selectedData != value)
                {
                    _selectedData = value;
                    OnPropertyChanged();
                    _selectedData.Snoop(Collector);
                }
            }
        }

        private ICommand _typesCommand = null;
        public ICommand TypesCommand
        {
            get
            {
                if (_typesCommand == null)
                {
                    _typesCommand = new RelayCommand<SnoopWindow>(
                        (window) => 
                        {
                            TypesWindow typesWindow = new TypesWindow();
                            typesWindow.ShowDialog();
                        }
                        );
                }
                return _closeCommand;
            }
        }

        private ICommand _closeCommand = null;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand<SnoopWindow>(
                        (window) => { window.Close(); }
                        );
                }
                return _closeCommand;
            }
        }

        private ICollector Collector { get; }

        public SnoopViewModel(ICollector collector, Data data)
        {
            Collector = collector;
            if (data is DataArray)
                Objects = new ObservableCollection<object>(((DataArray)data).Array);
            else
                Objects = new ObservableCollection<object>() { data.Value };
        }

        private void Snoop(object obj)
        {
            Datas.Clear();
            Task.Factory.StartNew(() =>
            {
                Collector.Stream(Datas, obj);
            }, System.Threading.CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
