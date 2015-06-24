using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Web.HtmlHelper.Attributes
{
    /// <summary>
    /// 是否在编辑的时候隐藏
    /// </summary>
    public class HiddenEdit :StyleAttribute
    {
        public override string getStyles(ViewMode mode)
        {
            if (mode == ViewMode.Edit)
            {
                return "display:none";
            }
            return base.getStyles(mode);
        }
    }
}
