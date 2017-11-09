using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace Ecommerce.Web.common.Helper.Extention
{
    public static class HtmlHelperExtention
    {
        public static string GetContent(this UrlHelper urlHelper, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                return string.Empty;
            else
                return urlHelper.Content(contentPath);
        }

        public static MvcHtmlString TextBoxForCustom<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool disabled = false)
        {
            IDictionary<string, object> values = TurnObjectIntoDictionary(htmlAttributes);

            if (disabled)
                values.Add("disabled", "true");


            return htmlHelper.TextBoxFor(expression, values);
        }

        public static MvcHtmlString TextAreaForCustom<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool disabled = false)
        {
            IDictionary<string, object> values = TurnObjectIntoDictionary(htmlAttributes);

            if (disabled)
                values.Add("disabled", "true");


            return htmlHelper.TextAreaFor(expression, values);
        }

        public static MvcHtmlString CheckBoxForCustom<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes, bool disabled = false)
        {
            IDictionary<string, object> values = TurnObjectIntoDictionary(htmlAttributes);

            if (disabled)
                values.Add("disabled", "true");


            return htmlHelper.CheckBoxFor(expression, values);
        }

        public static MvcHtmlString DropDownListForCustom<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null, bool disabled = false)
        {
            IDictionary<string, object> values = TurnObjectIntoDictionary(htmlAttributes);

            if (disabled)
                values.Add("disabled", "true");
            return htmlHelper.DropDownListFor(expression, selectList, values);
        }

        public static IDictionary<string, object> TurnObjectIntoDictionary(object data)
        {
            var attr = BindingFlags.Public | BindingFlags.Instance;
            var dict = new Dictionary<string, object>();
            if (data == null)
                return dict;
            foreach (var property in data.GetType().GetProperties(attr))
            {
                if (property.CanRead)
                {
                    dict.Add(property.Name, property.GetValue(data, null));
                }
            }
            return dict;

        }
    }
}