using MaterialDesignInPrism.ColorPicker.ViewModels;
using MaterialDesignInPrism.ColorPicker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MaterialDesignInPrism.ColorPicker
{
    public class ColorPickerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PickerView, PickerViewModel>();
            containerRegistry.RegisterForNavigation<SettingView, SettingViewModel>();
        }
    }
}
