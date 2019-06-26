using System;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;

using NwLookup.Snoop.Collectors;
using NwLookup.Snoop.Datas;

namespace NwLookup.Snoop.Views
{
    public partial class SnoopWindow : Window
    {
        public SnoopWindow(ICollector collector, Data data)
        {
            InitializeComponent();
            SourceInitialized += ExportWindow_SourceInitialized;
            DataContext = new SnoopViewModel(collector, data);
        }

        private void Types_Click(object sender, RoutedEventArgs e)
        {
            TypesWindow window = new TypesWindow();
            window.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;
        private const int WS_MINIMIZEBOX = 0x20000;

        private void ExportWindow_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper((Window)sender).Handle;
            var value = GetWindowLong(hwnd, GWL_STYLE);
            SetWindowLong(hwnd, GWL_STYLE, (int)((int)(value & ~WS_MAXIMIZEBOX) & ~WS_MINIMIZEBOX));
        }
    }
}
