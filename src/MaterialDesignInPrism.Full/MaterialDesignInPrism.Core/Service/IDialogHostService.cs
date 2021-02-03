using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.Core.Service
{
    /// <summary>
    /// 对话主机服务接口
    /// </summary>
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters = null, string IdentifierName = "Root");
    }
}
