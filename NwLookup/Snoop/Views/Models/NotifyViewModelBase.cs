using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NwLookup.Snoop.Views.Models
{
    public abstract class NotifyViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
