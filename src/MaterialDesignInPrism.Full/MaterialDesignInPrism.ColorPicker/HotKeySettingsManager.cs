using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.ColorPicker
{
    public class HotKeySettingsManager
    {
        private static HotKeySettingsManager m_Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static HotKeySettingsManager Instance
        {
            get { return m_Instance ?? (m_Instance = new HotKeySettingsManager()); }
        }

        /// <summary>
        /// 通知注册系统快捷键委托
        /// </summary>
        /// <param name="hotKeyModelList"></param>
        public delegate bool RegisterGlobalHotKeyHandler(HotKeyModel hotKeyModelList);
        public event RegisterGlobalHotKeyHandler RegisterGlobalHotKeyEvent;
        public bool RegisterGlobalHotKey(HotKeyModel hotKeyModelList)
        {
            if (RegisterGlobalHotKeyEvent != null)
            {
                return RegisterGlobalHotKeyEvent(hotKeyModelList);
            }
            return false;
        }
    }
}
