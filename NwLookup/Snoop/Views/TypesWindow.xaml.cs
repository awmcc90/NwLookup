using System;
using System.Windows;

namespace NwLookup.Snoop.Views
{
    public partial class TypesWindow : Window
    {
        public TypesWindow()
        {
            InitializeComponent();
            DataContext = new TypesViewModel();
        }
    }
}
