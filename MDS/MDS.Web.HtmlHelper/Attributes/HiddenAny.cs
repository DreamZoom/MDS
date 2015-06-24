using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Web.HtmlHelper.Attributes
{
    public class HiddenAny : StyleAttribute
    {
        public override string getStyles(ViewMode mode)
        {
            return "display:none";
        }
    }
}
