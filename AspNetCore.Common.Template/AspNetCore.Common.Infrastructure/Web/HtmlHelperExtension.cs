using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Common.Infrastructure.Pagination;
using AspNetCore.Common.Infrastructure.Extension;

namespace AspNetCore.Common.Infrastructure.Web
{
    public static class HtmlHelperExtension
    {
        private const string pageQueryName = "pageIndex";

        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageList">分页的数据</param>
        /// <param name="routeValues">路由数据</param>
        /// <param name="maxPagerNumber">最大显示的页码个数</param>
        /// <returns></returns>
        public static IHtmlContent RenderPager(this IHtmlHelper htmlHelper, IPagedList pageList,
            IDictionary<string, object> routeValues = null, int maxPagerNumber = 10)
        {
            if (pageList == null)
            {
                return new HtmlString(null);
            }
            var htmlResponse = new StringBuilder();
            TextWriter htmlWriter = new StringWriter(htmlResponse);
            htmlWriter.WriteLine("<div class=\"pageDiv\"><ul class=\"pagination\">");
            var canPrev = pageList.Paged.PageIndex > 0 && pageList.Paged.PageIndex < pageList.Paged.PageCount;
            var canNext = pageList.Paged.PageIndex + 1 < pageList.Paged.PageCount;
            var pagerRouteValues = GetParams(htmlHelper);
            var pageIndexParamName = pagerRouteValues.Keys.FirstOrDefault(g => string.Equals(g, pageQueryName, StringComparison.CurrentCultureIgnoreCase));
            //路由字典数据中添加分页参数
            if (string.IsNullOrWhiteSpace(pageIndexParamName))
            {
                pageIndexParamName = pageQueryName;
                pagerRouteValues.Add(pageQueryName, 0);
            }
            //覆盖老路由字典数据
            if (routeValues != null)
            {
                foreach (var routeValue in routeValues)
                {
                    var key = pagerRouteValues.Keys.FirstOrDefault(g => string.Equals(g, routeValue.Key, StringComparison.CurrentCultureIgnoreCase));
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        pagerRouteValues[key] = routeValue.Value;
                    }
                    else
                    {
                        pagerRouteValues.Add(routeValue.Key, routeValue.Value);
                    }
                }
            }

            #region render pager number

            pagerRouteValues[pageIndexParamName] = 0;
            WritePagerNumber(htmlHelper, htmlWriter, "首页", canPrev, !canPrev ? "disabled" : string.Empty,
                pagerRouteValues);
            pagerRouteValues[pageIndexParamName] = pageList.Paged.PageIndex - 1;
            WritePagerNumber(htmlHelper, htmlWriter, "<上一页", canPrev, !canPrev ? "disabled" : string.Empty,
                pagerRouteValues);

            var min = 0; //要显示的页码最小值
            var max = 0; //要显示的页码最大值
            if (pageList.Paged.PageCount <= maxPagerNumber)
            {
                min = 0;
                max = pageList.Paged.PageCount;
            }
            else if (pageList.Paged.PageIndex < maxPagerNumber / 2)
            {
                min = 0;
                max = maxPagerNumber;
            }
            else if (pageList.Paged.PageCount - pageList.Paged.PageIndex > maxPagerNumber)
            {
                min = pageList.Paged.PageIndex - maxPagerNumber / 2;
                max = min + maxPagerNumber;
            }
            else
            {
                min = pageList.Paged.PageCount - 10;
                max = pageList.Paged.PageCount;
            }
            for (var i = min; i < max; i++)
            {
                pagerRouteValues[pageIndexParamName] = i;
                if (i == pageList.Paged.PageIndex)
                {
                    WritePagerNumber(htmlHelper, htmlWriter, (i + 1).ToString(), false, "active", pagerRouteValues);
                }
                else
                {
                    WritePagerNumber(htmlHelper, htmlWriter, (i + 1).ToString(), true, null, pagerRouteValues);
                }
            }
            pagerRouteValues[pageIndexParamName] = pageList.Paged.PageIndex + 1;
            WritePagerNumber(htmlHelper, htmlWriter, "下一页>", canNext, !canNext ? "disabled" : string.Empty,
                pagerRouteValues);
            pagerRouteValues[pageIndexParamName] = pageList.Paged.PageCount - 1;
            WritePagerNumber(htmlHelper, htmlWriter, "尾页", canNext, !canNext ? "disabled" : string.Empty,
                pagerRouteValues);

            #endregion render pager number

            htmlWriter.WriteLine(string.Format(
                "<span class=\"pagination-total f-color\">共{1}页, {0}条.</span></div></ul>", pageList.Paged.TotalCount,
                pageList.Paged.PageCount));

            return new HtmlString(htmlResponse.ToString());
        }

        public static IHtmlContent ForInput(this IHtmlHelper htmlHelper, InputType type, string name,
            string value = null, string id = null)
        {
            var queriesKeyValues = GetParams(htmlHelper);
            var paramValue = queriesKeyValues.Keys.Contains(name, StringComparer.CurrentCultureIgnoreCase) ?
                queriesKeyValues.FirstOrDefault(g => string.Equals(g.Key, name, StringComparison.CurrentCultureIgnoreCase)).Value : null;
            if (type == InputType.Radio)
            {
                return ForRadio(htmlHelper, name, value,
                    string.Equals(value, paramValue));
            }
            if (type == InputType.CheckBox)
            {
                return ForCheckbox(htmlHelper, name,
                     string.Equals("1", paramValue));
            }
            var inputValue = value ?? paramValue;
            return
                new HtmlString(
                    string.Format(
                        "<input type=\"{0}\" name=\"{1}\" id=\"{3}\" class=\"form-control\" value=\"{2}\" />", type,
                        name, inputValue, id ?? name));
        }

        public static IHtmlContent ForInputReadOnly(this IHtmlHelper htmlHelper, InputType type, string name,
            string value = null, string cssCls = null)
        {
            var queriesKeyValues = GetParams(htmlHelper);
            var paramValue = queriesKeyValues.Keys.Contains(name, StringComparer.CurrentCultureIgnoreCase) ?
               queriesKeyValues.FirstOrDefault(g => string.Equals(g.Key, name, StringComparison.CurrentCultureIgnoreCase)).Value : null;
            if (type == InputType.Radio)
            {
                return ForRadio(htmlHelper, name, value,
                    string.Equals(value, paramValue), true);
            }
            if (type == InputType.CheckBox)
            {
                return ForCheckbox(htmlHelper, name,
                   string.Equals("1", paramValue), true);
            }
            var inputValue = value ?? paramValue;
            return new HtmlString(string.Format(
                        "<input type=\"{0}\" name=\"{1}\" id=\"{1}\" class=\"form-control {3}\" value=\"{2}\" readonly/>",
                        type, name, inputValue, cssCls));
        }

        private static IHtmlContent ForRadio(this IHtmlHelper htmlHelper, string name, string value, bool isChecked, bool readOnly = false)
        {
            return new HtmlString(string.Format("<input type=\"{0}\" name=\"{1}\" class=\"form-control\" value=\"{2}\"{3}{4}/>",
                        InputType.Radio, name, value, isChecked ? " checked" : null, readOnly ? " readonly" : null));
        }

        private static IHtmlContent ForCheckbox(this IHtmlHelper htmlHelper, string name, bool isChecked, bool readOnly = false)
        {
            return new HtmlString(string.Format(
                    "<input type=\"{0}\" name=\"{1}\" class=\"form-control\" value=\"1\"{2}{3}/>", InputType.Radio, name,
                    isChecked ? " checked" : null, readOnly ? " readonly" : null));
        }

        public static IHtmlContent ForImage(this IHtmlHelper htmlHelper, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HtmlString("");
            }
            var cfg =
                ActivatorUtilities.GetServiceOrCreateInstance<IConfiguration>(
                    htmlHelper.ViewContext.HttpContext.RequestServices);

            return
                new HtmlString(string.Format("<img src=\"{0}\" />",
                    Path.Combine(cfg.GetSection("AWSConfig")["S3FileHostUrl"], name)));
        }

        public static IHtmlContent ForEditImage(this IHtmlHelper htmlHelper, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HtmlString(@"<div class='upload-img-wrapper f-pt15 f-clearfix'></div>");
            }
            var cfg = ActivatorUtilities.GetServiceOrCreateInstance<IConfiguration>(htmlHelper.ViewContext.HttpContext.RequestServices);
            var divImg = @"<div class='upload-img-wrapper f-pt15 f-clearfix'>
                                <div class='img-item-new'>
                                    <div class='file-img-pane'>
                                        <div class='file-img-box'>
                                            <img src='{0}' class='imger' />
                                        </div>
                                    </div>
                                    <div class='file-actions'>
                                        <i class='ui-icons remove'></i>
                                    </div>
                                </div>
                            </div>";
            var url = Path.Combine(cfg.GetSection("AWSConfig")["S3FileHostUrl"], name);
            return new HtmlString(string.Format(divImg, url));
        }

        public static IHtmlContent ForInputSelect<T>(this IHtmlHelper htmlHelper, string name, IEnumerable<T> list,
            Func<T, string> IdSelector,
            Func<T, string> TextSelector,
            Func<T, string> PySelector = null,
            string selectedOption = null, string captionText = "-选择--")
        {
            var queriesKeyValues = GetParams(htmlHelper);
            var bindValue = selectedOption ??
                                   (
                                   queriesKeyValues.Keys.Contains(name, StringComparer.CurrentCultureIgnoreCase) ?
                                   queriesKeyValues.FirstOrDefault(g => string.Equals(g.Key, name, StringComparison.CurrentCultureIgnoreCase)).Value.ToString()
                                   : null
                                   );
            var selectHtml = new StringBuilder($"<div id=\"{name}\" class=\"input-selector form-control{(!string.IsNullOrWhiteSpace(bindValue) ? " choosed" : null)}\">");
            selectHtml.Append($"<div class=\"holder\"><span class=\"txt\">{{0}}</span><span class=\"caption\">{captionText}</span><i class=\"icon icon-angle-down\"></i><i class=\"icon clear icon-remove\"></i><input type=\"hidden\" value=\"{bindValue}\" name=\"{name}\"/></div>");
            selectHtml.Append("<div class=\"list-wrapper\"><input type=\"text\" class=\"form-control search\" /><ul class=\"list-items\">");
            if (list != null)
            {
                if (typeof(T) == typeof(int) || typeof(T) == typeof(double)
                    || typeof(T) == typeof(float) || typeof(T) == typeof(decimal)
                    || typeof(T) == typeof(string) || typeof(T) == typeof(long))
                {
                    foreach (var item in list)
                    {
                        if (bindValue == item.ToString())
                            captionText = item.ToString();
                        selectHtml.Append($"<li class=\"{((bindValue == item.ToString()) ? "active" : null)}\" data-id=\"{item}\">{item}</li>");
                    }
                }
                else
                {
                    var idList = list.Select(IdSelector).ToList();
                    var textList = list.Select(TextSelector).ToList();
                    var pyList = PySelector == null ? new List<string>() : list.Select(PySelector).ToList();
                    for (var i = 0; i < idList.Count; i++)
                    {
                        if ((bindValue == idList[i] || bindValue == textList[i]))
                            captionText = textList[i];
                        selectHtml.Append($"<li class=\"{((bindValue == idList[i] || bindValue == textList[i]) ? "active" : null)}\" data-id=\"{idList[i]}\" data-py=\"{(pyList.Count > i ? pyList[i] : null)}\">{textList[i]}</li>");
                    }
                }
            }
            selectHtml.Append("</ul></div></div>");
            return new HtmlString(string.Format(selectHtml.ToString(), captionText));
        }

        public static string GetAmazonFileUrl(this IHtmlHelper htmlHelper, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;
            var cfg =
                ActivatorUtilities.GetServiceOrCreateInstance<IConfiguration>(
                    htmlHelper.ViewContext.HttpContext.RequestServices);
            return Path.Combine(cfg.GetSection("AWSConfig")["S3FileHostUrl"], name);
        }

        public static string DisplayForDateTime(this IHtmlHelper htmlHelper, DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                return datetime.Value.ToString("yyyy-MM-dd");
            }
            return string.Empty;
        }

        /// <summary>
        /// 可输入的下拉框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <param name="selectedOption"></param>
        /// <returns></returns>
        public static IHtmlContent ForSelectEditor(this IHtmlHelper htmlHelper, string name, IEnumerable<string> options, string selectedOption = null, IEnumerable<KeyValuePair<string, string>> attributes = null)
        {
            var queriesKeyValues = GetParams(htmlHelper);
            var bindValue = selectedOption ??
                                   (
                                   queriesKeyValues.Keys.Contains(name, StringComparer.CurrentCultureIgnoreCase) ?
                                   queriesKeyValues.FirstOrDefault(g => string.Equals(g.Key, name, StringComparison.CurrentCultureIgnoreCase)).Value.ToString()
                                   : null
                                   );
            var selectHtml = new StringBuilder("<div class=\"m-select\">");
            var inputAttributes = attributes == null ? string.Empty : string.Join(" ", attributes.Select(g => string.Format(" {0}=\"{1}\"", g.Key, g.Value)));
            selectHtml.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"form-control select-txt J_validate\" value=\"{1}\" readonly data-toggle=\"dropdown\"{2}>", name, bindValue, inputAttributes);
            selectHtml.Append("<span class=\"select-wig\" data-toggle=\"dropdown\"><i class=\"caret\"></i></span>");
            selectHtml.Append("<span class=\"thumb-switch J_switcher\"><i class=\"iconfont-edit edit\"></i><i class=\"iconfont-search select f-dn\"></i></span>");
            selectHtml.Append("<ul class=\"dropdown-menu\">");
            if (options != null)
            {
                foreach (var option in options)
                {
                    selectHtml.AppendFormat("<li class=\"{1}\"><a data-val=\"{0}\" href=\"javascript:;\">{0}</a></li>", option, (selectedOption == option ? "active" : string.Empty));
                }
            }
            selectHtml.Append("</ul></div>");
            return new HtmlString(selectHtml.ToString());
        }

        public static IHtmlContent ForSelect(this IHtmlHelper htmlHelper, string name,
            Func<IEnumerable<KeyValuePair<string, string>>> options, bool hasCaption, string captionText,
            int selectedIndex)
        {
            var selectOptions = options();
            var selectHtml = new StringBuilder();
            selectHtml.AppendFormat("<select name=\"{0}\" id=\"{0}\" class=\"form-control\">", name);
            var i = 0;
            if (hasCaption)
            {
                selectHtml.Append("<option value=\"\">" + captionText + "</option>");
                i++;
            }
            foreach (var option in selectOptions)
            {
                selectHtml.AppendFormat("<option {2}value=\"{1}\">{0}</option>", option.Key, option.Value,
                    i == selectedIndex ? "selected " : null);
                i++;
            }
            selectHtml.Append("</select>");
            return new HtmlString(selectHtml.ToString());
        }

        public static IHtmlContent ForSelect(this IHtmlHelper htmlHelper, string name,
            Func<IEnumerable<KeyValuePair<string, string>>> options, bool hasCaption = true,
            string captionText = "--全部--", string selectedTextOrValue = null)
        {
            var selectedIndex = 0;
            var i = 0;
            var queriesKeyValues = GetParams(htmlHelper);
            selectedTextOrValue = selectedTextOrValue ??
                                  (
                                  queriesKeyValues.Keys.Contains(name, StringComparer.CurrentCultureIgnoreCase) ?
                                  queriesKeyValues.FirstOrDefault(g => string.Equals(g.Key, name, StringComparison.CurrentCultureIgnoreCase)).Value.ToString()
                                  : null
                                  );
            var selectOptions = options();
            if (!string.IsNullOrWhiteSpace(selectedTextOrValue))
            {
                if (hasCaption)
                    i++;
                foreach (var option in selectOptions)
                {
                    if (option.Key.Equals(selectedTextOrValue, StringComparison.CurrentCultureIgnoreCase) ||
                        option.Value.Equals(selectedTextOrValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        selectedIndex = i;
                        break;
                    }
                    i++;
                }
            }
            return ForSelect(htmlHelper, name, options, hasCaption, captionText, selectedIndex);
        }

        public static IHtmlContent ForSelect(this IHtmlHelper htmlHelper, string name, Type enumType, bool hasCaption = true,
           string captionText = "--全部--", string selectedTextOrValue = null)
        {
            var list = EnumExtension.GetList(enumType);
            return htmlHelper.ForSelect(name, () =>
            {
                return list.Select(g => new KeyValuePair<string, string>(g.Description, g.Value.ToString()));
            }, hasCaption, captionText, selectedTextOrValue);
        }

        public static IHtmlContent HiddenForQueryOrder(this IHtmlHelper htmlHelper)
        {
            string orderParameterName = "QueryOrder";
            var _urlParams = GetParams(htmlHelper);
            var _orderQueryVal = _urlParams.ContainsKey(orderParameterName) ? (string)_urlParams[orderParameterName] : null;

            return new HtmlString("<input type=\"hidden\" name=\"" + orderParameterName + "\" value=\"" + _orderQueryVal + "\" />");
        }

        //目前入驻使用，其他地方使用另行通知
        public static IHtmlContent AutoCompleteCitySelect(this IHtmlHelper htmlHelper,
            string selectedId = null,
            string provinceName = null, string cityName = null, string regionName = null)
        {
            var htmlResponse = new StringBuilder();
            string proviceId = null;
            string cityId = null;
            string countyId = null;
            if (selectedId != null)
            {
                if (selectedId.Length >= 6)
                {
                    proviceId = selectedId.Substring(0, 6);
                }
                if (selectedId.Length >= 9)
                {
                    cityId = selectedId.Substring(0, 9);
                }
                if (selectedId.Length >= 12)
                {
                    countyId = selectedId.Substring(0, 12);
                }
            }

            htmlResponse.AppendFormat(
                "<select style=\"width:100px;\" data-id=\"{0}\" name=\"{1}\" id=\"{2}\" class=\"province-list form-control\"></select>",
                proviceId, provinceName, provinceName?.Replace(".", "_"));
            htmlResponse.AppendFormat(
                "<select style=\"width:100px;margin:0 5px;\" data-id=\"{0}\" id=\"{2}\" name=\"{1}\" class=\"city-list form-control\"></select>",
                cityId, cityName, cityName?.Replace(".", "_"));
            htmlResponse.AppendFormat(
                "<select style=\"width:100px;margin:0 5px 0 0;\" data-id=\"{0}\" id=\"{2}\" name=\"{1}\" class=\"country-list form-control\"></select>",
                countyId, regionName, regionName?.Replace(".", "_"));

            return new HtmlString(htmlResponse.ToString());
        }

        public static IHtmlContent MoreFilters(this IHtmlHelper htmlHelper, string text = null)
        {
            var _params = GetParams(htmlHelper);
            var _paramName = "SMEXPAND";
            var _paramValue = _params.Keys.Contains(_paramName, StringComparer.CurrentCultureIgnoreCase) ?
                            _params.FirstOrDefault(g => string.Equals(g.Key, _paramName, StringComparison.CurrentCultureIgnoreCase)).Value.ToString() : null;
            return new HtmlString($"<div class=\"btn-more{(_paramValue == "1" ? " expand" : null)}\"><div class=\"shadow\"></div><span>{(text ?? "高级筛选")}</span><i class=\"icon-double-angle-down\"></i><input type=\"hidden\" name=\"{_paramName}\" value=\"{_paramValue}\" /></div>");
        }

        public static bool IsMoreFilterExpand(this IHtmlHelper htmlHelper)
        {
            var _params = GetParams(htmlHelper);
            var _paramName = "SMEXPAND";
            var _paramValue = _params.Keys.Contains(_paramName, StringComparer.CurrentCultureIgnoreCase) ?
                           _params.FirstOrDefault(g => string.Equals(g.Key, _paramName, StringComparison.CurrentCultureIgnoreCase)).Value.ToString() : null;
            return _paramValue == "1";
        }

        public static IHtmlContent OrderColumn(this IHtmlHelper htmlHelper, string text, string orderPropertyName)
        {
            string orderParameterName = "QueryOrder";
            var htmlResponse = new StringBuilder();
            TextWriter htmlWriter = new StringWriter(htmlResponse);

            string orderCls = null;

            var _urlParams = GetParams(htmlHelper);
            string orderQuery = _urlParams.ContainsKey(orderParameterName) ? (string)_urlParams[orderParameterName] : string.Empty;
            List<string> _orderParams = orderQuery.Split(new char[] { '_' }).ToList();

            bool has = false;
            if (_orderParams.Count % 2 == 0)
            {
                for (var i = 0; i < _orderParams.Count; i += 2)
                {
                    if (_orderParams[i] == orderPropertyName)
                    {
                        orderCls = _orderParams[i + 1];
                        _orderParams[i + 1] = _orderParams[i + 1] == "asc" ? "desc" : "asc";
                        has = true;
                    }
                }
                if (!has)
                {
                    _orderParams.Insert(0, "desc");
                    _orderParams.Insert(0, orderPropertyName);
                }
            }
            else
            {
                _orderParams = new List<string>() { orderPropertyName, "desc" };
            }

            _urlParams[orderParameterName] = string.Join("_", _orderParams);
            var action = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var controller = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            htmlHelper.ActionLink(text, action, controller, _urlParams, new { @class = orderCls }).WriteTo(htmlWriter, HtmlEncoder.Default);
            htmlResponse = htmlResponse.Insert(htmlResponse.Length - 4, "<i class=\"icon " + (orderCls == "asc" ? "icon-angle-up" : "icon-angle-down") + "\"></i>");
            return new HtmlString(htmlResponse.ToString());
        }

        private static void WritePagerNumber(IHtmlHelper htmlHelper, TextWriter htmlWriter, string pagerText,
            bool linkable, string classes = null, object routeValues = null)
        {
            var action = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var controller = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            htmlWriter.Write("<li class='page-item " + classes + "'>");
            if (linkable)
                htmlHelper.ActionLink(pagerText, action, controller, routeValues, new { @class = "page-link" }).WriteTo(htmlWriter, HtmlEncoder.Default);
            else
                htmlWriter.Write("<a href=\"javascript:;\" class=page-link>" + pagerText + "</a>");
            htmlWriter.Write("</li>");
        }

        private static IDictionary<string, object> GetParams(IHtmlHelper htmlHelper)
        {
            var _params = new Dictionary<string, object>();
            //获取url参数
            var queryParams = GetQueryStringParams(htmlHelper.ViewContext.HttpContext.Request.QueryString);
            //获取route参数
            var routeValues = htmlHelper.ViewContext.RouteData.Values;
            //获取view参数
            var viewParams = htmlHelper.ViewContext.ViewData;

            //url参数优先级最低
            foreach (var queryParam in queryParams)
            {
                _params.Add(queryParam.Key, queryParam.Value);
            }

            //使用route参数覆盖url参数
            if (routeValues != null)
            {
                foreach (var routeValue in routeValues)
                {
                    if (!IsCustomType(routeValue.Value.GetType()))
                    {
                        var key = _params.Keys.FirstOrDefault(g => string.Equals(g, routeValue.Key, StringComparison.CurrentCultureIgnoreCase));
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            _params[key] = routeValue.Value;
                        }
                        else
                        {
                            _params.Add(routeValue.Key, routeValue.Value);
                        }
                    }
                }
            }

            //使用view参数服务之前的参数
            if (viewParams != null)
            {
                foreach (var param in viewParams)
                {
                    if (param.Key == "Title")
                    {
                        continue;
                    }
                    if (!IsCustomType(param.Value.GetType()))
                    {
                        var key = _params.Keys.FirstOrDefault(g => string.Equals(g, param.Key, StringComparison.CurrentCultureIgnoreCase));
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            _params[key] = param.Value;
                        }
                        else
                        {
                            _params.Add(param.Key, param.Value);
                        }
                    }
                }
            }
            return _params;
        }

        private static IDictionary<string, string> GetQueryStringParams(QueryString queryString)
        {
            var keyValues = new Dictionary<string, string>();
            string queries = queryString.Value;
            if (queries.Length > 0)
            {
                var queryLetters = queries.Split(new[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
                string key = null;
                string value = null;
                string[] temp = null;
                foreach (var letter in queryLetters)
                {
                    temp = letter.Split('=');
                    key = temp[0];
                    value = temp.Length > 1 ? WebUtility.UrlDecode(temp[1]) : null;
                    keyValues.Add(key, value);
                }
            }
            return keyValues;
        }

        internal static bool IsCustomType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }
    }
}