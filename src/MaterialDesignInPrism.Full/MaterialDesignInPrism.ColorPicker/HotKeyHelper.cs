using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialDesignInPrism.ColorPicker
{
    /// <summary>
    /// 热键注册帮助
    /// </summary>
    public class HotKeyHelper
    {
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hotKeyModel">热键待注册项</param>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool RegisterHotKey(HotKeyModel hotKeyModel, IntPtr hWnd)
        {
            var fsModifierKey = new ModifierKeys();

            HotKeyManager.UnregisterHotKey(hWnd, HotKeyManager.COPY_ID);

            // 注册热键
            if (hotKeyModel.SelectType == EType.Alt)
                fsModifierKey = ModifierKeys.Alt;
            else if (hotKeyModel.SelectType == EType.Ctrl)
                fsModifierKey = ModifierKeys.Control;
            else if (hotKeyModel.SelectType == EType.Shift)
                fsModifierKey = ModifierKeys.Shift;

            var result = HotKeyManager.RegisterHotKey(hWnd, HotKeyManager.COPY_ID, fsModifierKey, (int)hotKeyModel.SelectKey);
            if (result) { RegistryManager.Register(hotKeyModel); }
            return result;
        }

        /// <summary>
        /// 注册注册表热键
        /// </summary>
        /// <param name="hWnd"></param>
        public static void DefaultRegisterHotKey(IntPtr hWnd)
        {
            RegisterHotKey(RegistryManager.ReadKey(), hWnd);
        }
    }
}
