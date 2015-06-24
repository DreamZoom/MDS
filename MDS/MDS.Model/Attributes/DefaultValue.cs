using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Model.Attributes
{
    public class DefaultValue : Attribute
    {
        public object Value { get; set; }
        public DefaultValue(object value)
        {
            this.Value = value;
        }
    }
}
