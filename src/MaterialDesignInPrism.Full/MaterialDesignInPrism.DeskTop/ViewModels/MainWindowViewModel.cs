using MaterialDesignInPrism.Core.Service;
using MaterialDesignInPrism.Core.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using Prism.Modularity;
using System.Reflection;
using System;

namespace MaterialDesignInPrism.DeskTop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IModuleCatalog moduleCatalog;

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
            this.moduleCatalog = moduleCatalog;
            this.regionManager = regionManager;
            ModuleInfos = new ObservableCollection<ModuleInfo>();
            InitModuleConfig();
            NavigationCommand = new DelegateCommand<string>(NavigationPage);
        }

        public DelegateCommand<string> NavigationCommand { get; private set; }

        private ObservableCollection<ModuleInfo> moduleInfos;

        public ObservableCollection<ModuleInfo> ModuleInfos
        {
            get { return moduleInfos; }
            set { moduleInfos = value; RaisePropertyChanged(); }
        }

        public void InitModuleConfig()
        {
            var modules = moduleCatalog.Modules;

            foreach (var item in modules)
            {
                var itr = item.ModuleName.Split(",");
                ModuleInfos.Add(new ModuleInfo()
                {
                    ModuleCode = itr[1],
                    ModuleName = itr[0]
                });
            }
        }

        private void NavigationPage(string obj)
        {
            regionManager.Regions["ContentRegion"].RequestNavigate(obj);
        }

    }
}
