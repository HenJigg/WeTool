using MaterialDesignInPrism.FontPicker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MaterialDesignInPrism.FontPicker
{
    public class FontPickerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FonPickerView>();
        }
    }
}