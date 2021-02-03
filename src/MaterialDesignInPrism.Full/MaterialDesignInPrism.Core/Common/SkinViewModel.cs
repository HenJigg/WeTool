using MaterialDesignColors;
using MaterialDesignInPrism.Core.Extensions;
using MaterialDesignInPrism.Core.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MaterialDesignInPrism.Core.Common
{
    /// <summary>
    /// 系统样式设置
    /// </summary>
    public class SkinViewModel : BindableBase, INavigationAware
    {
        public SkinViewModel()
        {
            Styles = new ObservableCollection<Color>();

            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
            ToggleBaseCommand = new DelegateCommand<object>(ApplyBase);
        }

        PaletteHelper _paletteHelper;

        public string SelectPageTitle { get; } = "个性化设置";

        private ObservableCollection<Color> _styles;

        public ObservableCollection<Color> Styles
        {
            get { return _styles; }
            set { _styles = value; RaisePropertyChanged(); }
        }

        //改变颜色
        public DelegateCommand<object> ChangeHueCommand { get; }

        //改变主题
        public DelegateCommand<object> ToggleBaseCommand { get; }

        private void ApplyBase(object isDark)
        {
            ModifyTheme(theme => theme.SetBaseTheme((bool)isDark ? Theme.Dark : Theme.Light));
        }

        private void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }

        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            _paletteHelper.ChangePrimaryColor(hue);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var swatches = SwatchHelper.Swatches.ToList().SelectMany(t => t.Hues);

            foreach (var item in swatches) Styles.Add(item);

            _paletteHelper = new PaletteHelper();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
