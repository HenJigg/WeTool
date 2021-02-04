using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignInPrism.Core.Common
{
    public class Inf : Attribute
    {
        public Inf(string Name,string Code)
        {
            this.Name = Name;
            this.Code = Code;
        }

        public string Name { get; }
        public string Code { get; }
    }
}
