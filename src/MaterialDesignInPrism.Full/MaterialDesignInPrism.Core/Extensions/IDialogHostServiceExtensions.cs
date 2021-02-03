using MaterialDesignInPrism.Core.Service;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.Core.Extensions
{
    /// <summary>
    /// 主机对话服务扩展类
    /// </summary>
    public static class IDialogHostServiceExtensions
    {
        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="hostService"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IDialogResult Question(this IDialogHostService hostService, string message)
        {
            return hostService.ShowDialog("QuestionView", new DialogParameters()
            {
                { "Value",message }
            }).Result;
        }
    }
}
