using MaterialDesignInPrism.Core.Config;
using MaterialDesignInPrism.Core.Service;
using MaterialDesignInPrism.Core.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MaterialDesignInPrism.DeskTop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IDialogHostService dialog;
        private readonly IRegionNavigationJournal journal;

        public MainWindowViewModel(IRegionManager regionManager, IDialogHostService dialog)
        {
            this.dialog = dialog;
            this.regionManager = regionManager;
            HomeCommand = new DelegateCommand(GoHome);
            MovePrevCommand = new DelegateCommand(MovePrev);
            MoveNextCommand = new DelegateCommand(MoveNext);
            NavigationCommand = new DelegateCommand<ModuleConfig>(NavigationPage);

            _moduleConfigs = CreateModuleConfigs();
        }

        #region Command

        public DelegateCommand HomeCommand { get; private set; }
        public DelegateCommand MovePrevCommand { get; private set; }
        public DelegateCommand MoveNextCommand { get; private set; }
        public DelegateCommand<ModuleConfig> NavigationCommand { get; private set; }

        #endregion

        #region Property

        private ModuleConfig _selectModuleConfig;

        public ModuleConfig SelectModuleConfig
        {
            get { return _selectModuleConfig; }
            set { _selectModuleConfig = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Method

        /// <summary>
        /// 下一步
        /// </summary>
        private void MoveNext()
        {
            if (journal != null && journal.CanGoForward)
                journal.GoForward();
        }

        /// <summary>
        /// 上一步
        /// </summary>
        private void MovePrev()
        {
            if (journal != null && journal.CanGoBack)
                journal.GoBack();
        }

        /// <summary>
        /// 返回首页
        /// </summary>
        private void GoHome()
        {
            NavigationPage("HomeView");
        }

        private void NavigationPage(ModuleConfig config)
        {
            if (config == null) return;

            SelectModuleConfig = config;
            SelectModuleConfig.IsActive = false;

            NavigationPage(config.Code);
        }

        /// <summary>
        /// 导航页
        /// </summary>
        /// <param name="moduleName"></param>
        void NavigationPage(string moduleName)
        {
            if (string.IsNullOrWhiteSpace(moduleName)) return;

            var result = regionManager.Regions["ContentRegion"].Navigate(moduleName);
        }

        /// <summary>
        /// 应用菜单
        /// </summary>
        /// <returns></returns>
        ObservableCollection<ModuleConfig> CreateModuleConfigs()
        {
            return new ObservableCollection<ModuleConfig>
            {
                new ModuleConfig("个性化","SkinView","ColorLens")
            };
        }

        #endregion

        private ObservableCollection<ModuleConfig> _moduleConfigs;

        public ObservableCollection<ModuleConfig> ModuleConfigs
        {
            get { return _moduleConfigs; }
            set { _moduleConfigs = value; RaisePropertyChanged(); }
        }

    }
}
