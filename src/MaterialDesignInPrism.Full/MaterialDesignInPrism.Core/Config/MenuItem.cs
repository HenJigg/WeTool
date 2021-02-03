using Prism.Mvvm;

namespace MaterialDesignInPrism.Core.Config
{
    public class ModuleConfig : BindableBase
    {
        public ModuleConfig(string name, string code, string kind)
        {
            _code = code;
            _name = name;
            _kind = kind;
        }

        private string _name;
        private string _code;
        private string _kind;
        private bool isActive;

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; RaisePropertyChanged(); }
        }

        public string Kind
        {
            get { return _kind; }
            set { _kind = value; RaisePropertyChanged(); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; RaisePropertyChanged(); }
        }
    }
}
