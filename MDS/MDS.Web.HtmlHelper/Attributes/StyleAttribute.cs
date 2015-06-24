using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Web.HtmlHelper.Attributes
{
    public class StyleAttribute : Attribute
    {
        /// <summary>
        /// 获取样式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public virtual string getStyles(ViewMode mode)
        {
            return string.Empty;
        }
    }
}
