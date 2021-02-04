using MaterialDesignInPrism.Core.Common;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignInPrism.FontPicker.ViewModels
{
    public class FonPickerViewModel : BindableBase
    {
        public FonPickerViewModel(IEventAggregator aggregator)
        {
            GridFontList = new ObservableCollection<FontItem>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.aggregator = aggregator;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "打开文件":
                    {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter = "所有文件(*.ttf)|*.ttf";
                        var result = dialog.ShowDialog();
                        if((bool)result) AddFile(dialog.FileName);
                    }
                    break;
                case "清空":
                    GridFontList.Clear();
                    break;
                default:
                    Clipboard.SetDataObject(obj, false);
                    aggregator.GetEvent<StringEvent>().Publish("复制成功!");
                    break;
            }
        }

        #region Property

        private FontFamily fontFamily;
        public FontFamily FontFamily
        {
            get { return fontFamily; }
            set { SetProperty(ref fontFamily, value); }
        }

        private ObservableCollection<FontItem> gridFontList;
        private readonly IEventAggregator aggregator;

        public ObservableCollection<FontItem> GridFontList
        {
            get { return gridFontList; }
            set { gridFontList = value; RaisePropertyChanged(); }
        }


        #endregion

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public void AddFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            if (!File.Exists(filePath))
                return;

            var filedic = System.IO.Path.GetDirectoryName(filePath);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            FontFamily = new FontFamily($"{filedic}\\#{fileName}");
            GridFontList.Clear();
            GlyphTypeface glyph = new GlyphTypeface(new System.Uri(filePath));
            var maps = glyph.CharacterToGlyphMap;
            foreach (var kvp in maps)
            {
                int Key = kvp.Key;
                if (Key > 0xffff) break;
                byte[] bytes = new byte[] { (byte)Key, (byte)(Key >> 8) };
                string Character = ByteToString(bytes);
                GridFontList.Add(new FontItem() { Key = $"&#X{kvp.Key.ToString("X")}".ToLower(), Value = Character });
            }

            if (GridFontList.Count > 0) aggregator.GetEvent<StringEvent>().Publish("加载成功!");
        }

        static string ByteToString(byte[] array)
        {
            var enc = Encoding.Unicode;
            var chars = enc.GetChars(array);
            return new string(chars);
        }
    }
}
