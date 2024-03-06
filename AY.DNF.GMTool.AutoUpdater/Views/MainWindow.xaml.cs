using AY.DNF.GMTool.AutoUpdater.ViewModels;
using System.Windows;

namespace AY.DNF.GMTool.AutoUpdater.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string url)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                (DataContext as MainWindowViewModel)!.Start(url);
            };
        }
    }
}