using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Web.HtmlHelper.Attributes
{
    /// <summary>
    /// 在display时隐藏
    /// </summary>
    public class HiddenDisplay : StyleAttribute
    {
        public override string getStyles(ViewMode mode)
        {
            if (mode == ViewMode.Display)
            {
                return "display:none";
            }
            return base.getStyles(mode);
        }
    }
}
