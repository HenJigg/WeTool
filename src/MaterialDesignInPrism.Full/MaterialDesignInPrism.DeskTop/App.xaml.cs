using MaterialDesignInPrism.ColorPicker;
using MaterialDesignInPrism.Core.Common;
using MaterialDesignInPrism.Core.Service;
using MaterialDesignInPrism.DeskTop.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace MaterialDesignInPrism.DeskTop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<IColorPickerModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
