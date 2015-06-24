using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using MDS.Web.HtmlHelper;
using MDS.Web.HtmlHelper.Attributes;
using System.Reflection;

namespace System.Web.Mvc
{

    public static class HtmlExtensions
    {
        public static MvcHtmlString EditModel(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            ModelMetadata metadata = helper.ViewData.ModelMetadata;

            Type type = helper.ViewData.ModelMetadata.ModelType;
            foreach (var p in metadata.Properties)
            {
                var property = type.GetProperty(p.PropertyName);
                if (property == null) continue;
                if (IsRemove(property, ViewMode.Edit)) continue;
                var styles = getStyles(property, ViewMode.Edit);

                sb.Append(string.Format("<div class=\"{1}\" style=\"{0}\">", styles,"model-box"));
                sb.Append(string.Format("<div class=\"edit-label\" >{0}</div>", p.DisplayName ?? p.PropertyName));
                sb.Append(string.Format("<div class=\"edit-filed\">{0}</div>", helper.Editor(p.PropertyName)));
                sb.Append("</div>");
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString DisplayModel(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            ModelMetadata metadata = helper.ViewData.ModelMetadata;

            Type type = helper.ViewData.ModelMetadata.ModelType;
            foreach (var p in metadata.Properties)
            {
                var property = type.GetProperty(p.PropertyName);
                if (property == null) continue;
                if (IsRemove(property, ViewMode.Display)) continue;
                var styles = getStyles(property, ViewMode.Display);

                sb.Append(string.Format("<div class=\"{1}\" style=\"{0}\">", styles, "model-box"));
                sb.Append(string.Format("<div class=\"dispaly-label\" >{0}</div>", p.DisplayName ?? p.PropertyName));
                sb.Append(string.Format("<div class=\"dispaly-filed\">{0}</div>", helper.Display(p.PropertyName)));
                sb.Append("</div>");
            }
            return new MvcHtmlString(sb.ToString());
        }


        private static string getStyles(PropertyInfo property,ViewMode mode)
        {
            List<string> styles = new List<string>();
            var attrs = property.GetCustomAttributes(typeof(StyleAttribute), true);
            foreach (var attr in attrs)
            {
                var attribute = attr as StyleAttribute;
                styles.Add(attribute.getStyles(mode));
            }
            return string.Join(";", styles.ToArray());
        }

        private static bool IsRemove(PropertyInfo property, ViewMode mode)
        {
            RemoveAttribute reattr = property.GetCustomAttribute<RemoveAttribute>();
            if (reattr == null) return false;
            return reattr.IsRemove(mode);
        }
    }
}
