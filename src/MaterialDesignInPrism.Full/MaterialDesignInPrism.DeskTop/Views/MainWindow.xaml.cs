using MaterialDesignInPrism.Core.Common;
using Prism.Events;
using System;
using System.Windows;

namespace MaterialDesignInPrism.DeskTop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IEventAggregator aggregator)
        {
            InitializeComponent();
            this.MouseMove += (s, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                    this.DragMove();
            };
            aggregator.GetEvent<StringEvent>().Subscribe(arg =>
            {
                if (string.IsNullOrWhiteSpace(arg))
                    this.WindowState = WindowState.Minimized;
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        SnackBar.MessageQueue.Enqueue(arg + "");
                    });
                }
            });

            this.btnmin.Click += (s, e) => this.WindowState = WindowState.Minimized;
            this.btnexit.Click += (s, e) => Environment.Exit(0);
        }
    }
}
