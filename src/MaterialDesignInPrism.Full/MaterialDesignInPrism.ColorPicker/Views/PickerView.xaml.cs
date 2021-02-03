using MaterialDesignInPrism.ColorPicker.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace MaterialDesignInPrism.ColorPicker.Views
{
    /// <summary>
    /// ColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class PickerView : UserControl, IDisposable
    {
        CancellationTokenSource colorToken;
        private IntPtr m_Hwnd = new IntPtr();

        public PickerView()
        {
            InitializeComponent();
            ConfigurationService();
            this.Loaded += PickerView_Loaded;
            HotKeySettingsManager.Instance.RegisterGlobalHotKeyEvent += Instance_RegisterGlobalHotKeyEvent;
        }

        private void PickerView_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取窗体句柄
            m_Hwnd = ((HwndSource)PresentationSource.FromVisual(this)).Handle;
            HwndSource hWndSource = HwndSource.FromHwnd(m_Hwnd);
            // 添加处理程序
            if (hWndSource != null) hWndSource.AddHook(WndProc);
            HotKeyHelper.DefaultRegisterHotKey(m_Hwnd);
        }

        /// <summary>
        /// 窗体回调函数，接收所有窗体消息的事件处理函数
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="msg">消息</param>
        /// <param name="wideParam">附加参数1</param>
        /// <param name="longParam">附加参数2</param>
        /// <param name="handled">是否处理</param>
        /// <returns>返回句柄</returns>
        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wideParam, IntPtr longParam, ref bool handled)
        {
            switch (msg)
            {
                case HotKeyManager.WM_HOTKEY:
                    string value = txtValue.Text;
                    Clipboard.SetDataObject(txtValue.Text, false);
                    (this.DataContext as PickerViewModel).Add(value);
                    SnackBar.MessageQueue.Enqueue($"{value}复制成功!");
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        #region Register

        /// <summary>
        /// 通知注册系统快捷键事件处理函数
        /// </summary>
        /// <param name="hotKeyModelList"></param>
        /// <returns></returns>
        private bool Instance_RegisterGlobalHotKeyEvent(HotKeyModel hotKeyModel)
        {
            return InitHotKey(hotKeyModel);
        }

        /// <summary>
        /// 初始化注册快捷键
        /// </summary>
        /// <param name="hotKeyModelList">待注册热键的项</param>
        /// <returns>true:保存快捷键的值；false:弹出设置窗体</returns>
        private bool InitHotKey(HotKeyModel hotKeyModel = null)
        {
            // 注册全局快捷键
            return HotKeyHelper.RegisterHotKey(hotKeyModel, m_Hwnd);
        }


        #endregion

        void ConfigurationService()
        {
            colorToken = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!colorToken.IsCancellationRequested)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var colorSelect = ColorPickerManager.GetColor();
                        var newcolor = System.Windows.Media.Color.FromArgb(colorSelect.A, colorSelect.R, colorSelect.G, colorSelect.B);
                        borColor.Background = new System.Windows.Media.SolidColorBrush(newcolor);
                    });
                    await Task.Delay(200);
                }
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            colorToken.Cancel();
        }
    }

    /// <summary>  
    /// 获取当前光标处颜色，win8下wpf测试成功  
    /// </summary>  
    public class ColorPickerManager
    {
        public static Color GetColor()
        {
            POINT p = new POINT();
            GetCursorPos(out p);
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, p.X, p.Y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
        IntPtr hdcDest, // 目标设备的句柄   
        int nXDest, // 目标对象的左上角的X坐标   
        int nYDest, // 目标对象的左上角的X坐标   
        int nWidth, // 目标对象的矩形的宽度   
        int nHeight, // 目标对象的矩形的长度   
        IntPtr hdcSrc, // 源设备的句柄   
        int nXSrc, // 源对象的左上角的X坐标   
        int nYSrc, // 源对象的左上角的X坐标   
        int dwRop // 光栅的操作值   
        );

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(
        string lpszDriver, // 驱动名称   
        string lpszDevice, // 设备名称   
        string lpszOutput, // 无用，可以设定位"NULL"   
        IntPtr lpInitData // 任意的打印机数据   
        );

        /// <summary>  
        /// 获取指定窗口的设备场景  
        /// </summary>  
        /// <param name="hwnd">将获取其设备场景的窗口的句柄。若为0，则要获取整个屏幕的DC</param>
        /// <returns>指定窗口的设备场景句柄，出错则为0</returns>  
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>  
        /// 释放由调用GetDC函数获取的指定设备场景  
        /// </summary>  
        /// <param name="hwnd">要释放的设备场景相关的窗口句柄</param>  
        /// <param name="hdc">要释放的设备场景句柄</param>  
        /// <returns>执行成功为1，否则为0</returns>  
        [DllImport("user32.dll")]
        private static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        /// <summary>  
        /// 在指定的设备场景中取得一个像素的RGB值  
        /// </summary>  
        /// <param name="hdc">一个设备场景的句柄</param>  
        /// <param name="nXPos">逻辑坐标中要检查的横坐标</param>  
        /// <param name="nYPos">逻辑坐标中要检查的纵坐标</param>  
        /// <returns>指定点的颜色</returns>  
        [DllImport("gdi32.dll")]
        private static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
    }
}
