using AY.DNF.GMTool.AutoUpdater.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using TiaoTiaoCode.NLogger;

namespace AY.DNF.GMTool.AutoUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        string _url;
        protected override void OnStartup(StartupEventArgs e)
        {
            TiaoTiaoNLogger.FastNoDatabaseInit();

            if (e.Args.Length <= 0)
                _url = string.Empty;
            else
                _url = e.Args[0];

            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>((typeof(string), _url));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
