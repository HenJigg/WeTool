using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.ColorPicker
{
    public class RegistryManager
    {
        public readonly static RegistryKey ColorPickerKey = Registry.CurrentUser.CreateSubKey("ColorPickerKey");

        public static void Register(HotKeyModel model)
        {
            var config = ColorPickerKey.CreateSubKey("Config");
            config.SetValue("Type", model.SelectType);
            config.SetValue("Key", model.SelectKey);
        }

        public static HotKeyModel ReadKey()
        {
            var config = ColorPickerKey.OpenSubKey("Config");
            if (config == null)
                return new HotKeyModel() { SelectType = EType.Ctrl, SelectKey = EKey.A };
            EType type = EType.Ctrl;
            EKey eKey = EKey.A;
            Enum.TryParse<EType>(config.GetValue("Type").ToString(), true, out type);
            Enum.TryParse<EKey>(config.GetValue("Key").ToString(), true, out eKey);
            return new HotKeyModel() { SelectKey = eKey, SelectType = type };
        }
    }
}
