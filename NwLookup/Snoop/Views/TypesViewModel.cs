using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NwLookup.Snoop.Collectors;
using NwLookup.Snoop.Views.Models;

namespace NwLookup.Snoop.Views
{
    public class TypesViewModel : NotifyViewModelBase
    {
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged();
                GetResults(_search);
            }
        }

        private List<Type> _orderedTypes { get; }
        public ObservableCollection<Type> Types { get; }

        public TypesViewModel()
        {
            _orderedTypes = ReflectionUtils.Types.OrderBy(x => x.ToString()).ToList();
            Types = new ObservableCollection<Type>(_orderedTypes);
        }

        private void GetResults(string search)
        {
            Types.Clear();
            Task.Factory.StartNew(() =>
            {
                return _orderedTypes.Where(x => x.ToString().StartsWith(search));
            }).ContinueWith(task =>
            {
                //add the results to the source collection
                foreach (Type type in task.Result)
                    Types.Add(type);
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
