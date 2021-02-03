using MaterialDesignInPrism.Core.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.ColorPicker.ViewModels
{
    public class SettingViewModel : BindableBase, IDialogHostAware
    {
        public SettingViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            DialogHost.Close(IdentifierName);
        }

        private void Save()
        {
            HotKeySettingsManager.Instance.RegisterGlobalHotKey(new HotKeyModel()
            {
                SelectType = SelectType,
                SelectKey = SelectKey
            });
            DialogHost.Close(IdentifierName);
        }

        public Task OnDialogOpenedAsync(IDialogParameters parameters)
        {
            var hostKey = RegistryManager.ReadKey();
            SelectType = hostKey.SelectType;
            SelectKey = hostKey.SelectKey;
            return Task.FromResult(true);
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public Array Keys { get { return Enum.GetValues(typeof(EKey)); } }

        public Array Types { get { return Enum.GetValues(typeof(EType)); } }

        private EKey _selectKey;
        private EType _selectType;
        public EKey SelectKey
        {
            get { return _selectKey; }
            set { _selectKey = value; RaisePropertyChanged(); }
        }

        public EType SelectType
        {
            get { return _selectType; }
            set { _selectType = value; RaisePropertyChanged(); }
        }

        public string Title => "快捷键设置";

        public string IdentifierName { get; set; } = "Root";
    }
}
