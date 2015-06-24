using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Web.HtmlHelper.Attributes
{
    public class RemoveAttribute :  Attribute
    {
        public virtual bool IsRemove(ViewMode mode)
        {
            return true;
        }
    }
}
