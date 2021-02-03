using MaterialDesignInPrism.Core.Common;
using Prism.Events;
using System.Windows;

namespace MaterialDesignInPrism.DeskTop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEventAggregator aggregator;

        public MainWindow(IEventAggregator aggregator)
        {
            InitializeComponent();
            this.MouseMove += (s, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                    this.DragMove();
            };
            this.aggregator = aggregator;
            this.aggregator.GetEvent<StringEvent>().Subscribe(arg =>
            {
                this.WindowState = WindowState.Minimized;
            });
        }
    }
}
