using MaterialDesignInPrism.Core.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.Core.Mvvm
{
    /// <summary>
    /// 弹出式窗口公共基类
    /// </summary>
    public class BaseShowViewModel : BindableBase, IDialogHostAware
    {
        public BaseShowViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public string IdentifierName { get; set; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public virtual void Cancel()
        {
            Close(new DialogResult(ButtonResult.Cancel));
        }

        public virtual void Save()
        {
            Close(new DialogResult(ButtonResult.OK));
        }

        void Close(DialogResult result)
        {
            DialogHost.Close(IdentifierName, result);
        }

        public virtual Task OnDialogOpenedAsync(IDialogParameters parameters)
        {
            return Task.FromResult(true);
        }
    }
}
