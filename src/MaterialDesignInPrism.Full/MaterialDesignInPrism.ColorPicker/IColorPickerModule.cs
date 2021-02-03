using MaterialDesignInPrism.ColorPicker.ViewModels;
using MaterialDesignInPrism.ColorPicker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialDesignInPrism.ColorPicker
{
    public class IColorPickerModule : IModule
    {
        public IColorPickerModule(IRegionManager manager)
        {
            manager.RegisterViewWithRegion("ContentRegion", typeof(PickerView));
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PickerView, PickerViewModel>();
            containerRegistry.RegisterForNavigation<SettingView,SettingViewModel>();
        }
    }
}
