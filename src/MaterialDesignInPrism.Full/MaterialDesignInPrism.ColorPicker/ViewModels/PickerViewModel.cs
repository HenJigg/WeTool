using MaterialDesignInPrism.Core.Common;
using MaterialDesignInPrism.Core.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignInPrism.ColorPicker.ViewModels
{
    public class PickerViewModel : BindableBase
    {
        private readonly IDialogHostService dialogHost;
        private readonly IEventAggregator aggregator;

        public PickerViewModel(IDialogHostService dialogHost, IEventAggregator aggregator)
        {
            this.dialogHost = dialogHost;
            this.aggregator = aggregator;
            ColorList = new ObservableCollection<string>();
            CopyCommand = new DelegateCommand<string>(Copy);
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        private void Copy(string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return;
            Clipboard.SetDataObject(obj, false);
        }

        private ObservableCollection<string> _ColorList;

        /// <summary>
        /// 颜色列表
        /// </summary>
        public ObservableCollection<string> ColorList
        {
            get { return _ColorList; }
            set { _ColorList = value; RaisePropertyChanged(); }
        }

        public void Add(string value)
        {
            if (ColorList.FirstOrDefault(t => t.Equals(value)) == null)
                ColorList.Add(value);
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "最小化": aggregator.GetEvent<StringEvent>().Publish(""); break;
                case "退出": Environment.Exit(0); break;
                case "快捷键": NavigationPage("SettingView"); break;
            }
        }

        void NavigationPage(string pageName)
        {
            dialogHost.ShowDialog(pageName);
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<string> CopyCommand { get; private set; }
    }
}
