using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MvcRegionCity.HtmlHelpers
{

    public static class NavMenuHelper
    {
        public static MvcHtmlString NavMenuFor(this HtmlHelper html, string name, IEnumerable collection)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("dropdown");

            var a = new TagBuilder("a");
            a.InnerHtml += name;
            a.InnerHtml += "<br/><span></span>";
            a.MergeAttribute("data-toggle", "dropdown");
            a.MergeAttribute("href", "#");
            a.AddCssClass("dropdown-toggle");
            li.InnerHtml += a.ToString();

            var ul = new TagBuilder("ul");
            ul.AddCssClass("dropdown-menu");
            CreateMenu(ul, collection);
            li.InnerHtml += ul.ToString();
            return MvcHtmlString.Create(li.ToString());

        }
        public static void CreateMenu(TagBuilder root, IEnumerable list)
        {
            var type = list.GetType().GetGenericArguments()[0];
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var collection = properties.FirstOrDefault(p => p.PropertyType.GetTypeInfo().IsGenericType &&
                p.PropertyType.GetTypeInfo().ImplementedInterfaces.Any(n => n.GUID == typeof(IEnumerable<>).GUID));

            var name = properties.FirstOrDefault(p => p.Name.ToLower()
                .Contains(type.Name.ToLower() + "name"));


            if (collection != null)
            {
                foreach (var item in list)
                {
                    var li = new TagBuilder("li");

                    var a = new TagBuilder("a");
                    a.SetInnerText(name.GetValue(item).ToString());
                    a.MergeAttribute("data-toggle", "dropdown");
                    a.MergeAttribute("href", "#");
                    a.AddCssClass("dropdown-toggle");
                    li.InnerHtml += a.ToString();
                    
                    var col = collection.GetValue(item) as IEnumerable;
                    bool isNext = col.GetEnumerator().MoveNext();
                    if (isNext)
                    {
                        li.AddCssClass("dropdown dropdown-submenu");
                        var ul = new TagBuilder("ul");
                        ul.AddCssClass("dropdown-menu");
                        CreateMenu(ul, col);
                        li.InnerHtml += ul.ToString();
                    }
                    root.InnerHtml += li.ToString();
                }
            }
            else
                foreach (var item in list)
                {
                    var li = new TagBuilder("li");
                    var a = new TagBuilder("a");
                    a.MergeAttribute("href", "#");
                    a.SetInnerText(name.GetValue(item).ToString());
                    li.InnerHtml += a.ToString();
                    root.InnerHtml += li.ToString();

                }
        }
    }
}