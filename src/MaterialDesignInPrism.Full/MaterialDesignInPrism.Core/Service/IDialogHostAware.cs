using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.Core.Service
{
    /// <summary>
    /// 对话主机ViewModel基类
    /// </summary>
    public interface IDialogHostAware
    {
        /// <summary>
        /// DialogHost顶级节点
        /// </summary>
        string IdentifierName { get; set; }

        /// <summary>
        /// 页面初始化前传递参数事件
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task OnDialogOpenedAsync(IDialogParameters parameters);

        /// <summary>
        /// 确认
        /// </summary>
        DelegateCommand SaveCommand { get; }

        /// <summary>
        /// 取消
        /// </summary>
        DelegateCommand CancelCommand { get; }
    }
}
