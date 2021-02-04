using MaterialDesignInPrism.FontPicker.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignInPrism.FontPicker.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class FonPickerView : UserControl
    {
        public FonPickerView()
        {
            InitializeComponent();
            this.Drop += ViewA_Drop;
        }

        private void ViewA_Drop(object sender, DragEventArgs e)
        {
            string msg = string.Empty;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                msg = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            (this.DataContext as FonPickerViewModel).AddFile(msg);
        }
    }
}
