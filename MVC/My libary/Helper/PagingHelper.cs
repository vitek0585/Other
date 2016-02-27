using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcRegionCity.HtmlHelpers
{
    using GetUrl = Func<string, string>;

    public abstract class FactoryTagBase
    {
        public abstract TagBuilder ConstructTag(string tag);
    }
    public class FactoryTag : FactoryTagBase
    {
        private Config _config;
        public FactoryTag(Config config)
        {
            _config = config;
        }
        public override TagBuilder ConstructTag(string tagName)
        {
            var tag = new TagBuilder(tagName);

            var propeties = _config.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var attrs = propeties.Where(n => n.GetCustomAttribute<DescriptionTagAttribute>() != null
                && n.GetCustomAttribute<DescriptionTagAttribute>().IsStaticValue && n.GetValue(_config) != null).
                ToDictionary(key => key.GetCustomAttribute<DescriptionTagAttribute>().NameAttrTag,
                value => value.GetValue(_config).ToString().ToLowerInvariant());

            tag.MergeAttributes(attrs);

            tag.AddCssClass(_config.ButtonCss);

            return tag;
        }
    }
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

    }

    public class Config
    {
        [DescriptionTag("data-ajax-url", false)]
        public GetUrl AjaxUrl { get; set; }
        [DescriptionTag("href", false)]
        public GetUrl NoAjaxUrl { get; set; }
        private string _updateId;
        [DescriptionTag("data-ajax-update")]
        public string UpdateId
        {
            get { return _updateId; }
            set { _updateId = "#" + value; }
        }
        private string _loadingElemId;
        [DescriptionTag("data-ajax-loading")]
        public string LoadingElemId
        {
            get { return _loadingElemId; }
            set { _loadingElemId = "#" + value; }
        }

        [DescriptionTag("data-ajax-mode")]
        public string AjaxMode { get; set; }
        public string ButtonCss { get; set; }
        public string ButtonCssActive { get; set; }
        public string LeftPrev { get; set; }
        public string RightPrev { get; set; }
        [DescriptionTag("data-ajax")]
        public bool IsAsync { get; set; }


    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DescriptionTagAttribute : Attribute
    {
        public string NameAttrTag { get; set; }
        public bool IsStaticValue { get; set; }
        public DescriptionTagAttribute(string nameAttrTag, bool isStaticValue = true)
        {
            NameAttrTag = nameAttrTag;
            IsStaticValue = isStaticValue;
        }
    }
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, string pagingId, PagingInfo pagingInfo, Config config)
        {
            TagBuilder container = new TagBuilder("div");
            container.GenerateId(pagingId);

            container.MergeAttributes(new Dictionary<string, string>
            {
                {"data-total-pages",pagingInfo.TotalPages.ToString()},
                {"data-current-page",pagingInfo.TotalPages.ToString()},
            });

            var elems = Enumerable.Repeat(new Func<TagBuilder>(() => new FactoryTag(config).ConstructTag("a")), 9)
                .Select(m => m.Invoke()).ToList();

            CalculatePagind(elems, config, pagingInfo);
            elems.Where(i => string.IsNullOrEmpty(i.InnerHtml)).ToList().ForEach(SetHide);

            container.InnerHtml = elems.Aggregate(new StringBuilder(), (b, t) => b.AppendLine(t.ToString())).ToString();

            return MvcHtmlString.Create(container.ToString());
        }

        private static void SetHide(TagBuilder tag)
        {
            tag.MergeAttribute("style", "visibility:visible");
        }
        public static void CalculatePagind(IEnumerable<TagBuilder> elems, Config config, PagingInfo pagingInfo)
        {
            var current = pagingInfo.CurrentPage;
            int start = current - 2 > 3 ? current - 2 : 1;
            int currentItem = 0;
            if (start > 2)
            {
                SetTag(elems.ElementAt(currentItem++), config, 1.ToString());

                SetTag(elems.ElementAt(currentItem), config, (start - 1).ToString());
                elems.ElementAt(currentItem).MergeAttribute("data-prev","left");
                elems.ElementAt(currentItem++).SetInnerText(config.LeftPrev);
            }
            for (; start < current + 3 && start <= pagingInfo.TotalPages; currentItem++, start++)
            {
                if (start == current)
                {
                    elems.ElementAt(currentItem).AddCssClass(config.ButtonCssActive);
                    elems.ElementAt(currentItem).MergeAttribute("data-current-page",start.ToString());
                }

                SetTag(elems.ElementAt(currentItem), config, start.ToString());
            }

            if (start + 2 <= pagingInfo.TotalPages)
            {
                SetTag(elems.ElementAt(currentItem), config, start.ToString());
                elems.ElementAt(currentItem).MergeAttribute("data-prev", "right");
                elems.ElementAt(currentItem++).SetInnerText(config.RightPrev);

                SetTag(elems.ElementAt(currentItem), config, pagingInfo.TotalPages.ToString());

            }
            else
            {
                while (start <= pagingInfo.TotalPages)
                {
                    SetTag(elems.ElementAt(currentItem++), config, (start++).ToString());
                }
            }

        }

        private static void SetTag(TagBuilder elem, Config config, string num)
        {
            var propeties = config.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var attr = propeties.Where(n => n.GetCustomAttribute<DescriptionTagAttribute>() != null
                && !n.GetCustomAttribute<DescriptionTagAttribute>().IsStaticValue && n.GetValue(config) != null).
                ToDictionary(key => key.GetCustomAttribute<DescriptionTagAttribute>().NameAttrTag,
                value => (value.GetValue(config) as GetUrl)(num));

            elem.MergeAttributes(attr);

            elem.SetInnerText(num);
        }

    }

}
