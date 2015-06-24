using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MDS.Web.HtmlHelper
{
    public class AttributeHelper
    {
        public static bool IsApplyAttribute(PropertyInfo p, Type attributeType)
        {
            Attribute attr = p.GetCustomAttribute(attributeType);
            return attr!=null;
        }
    }
}
