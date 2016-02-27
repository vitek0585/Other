using System;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace MvcRegionCity.Extensions
{
    public static class HelperMethods
    {
        private const string _widgetVk = "VK.Widgets.";

        public static string StringModernize(this string str, params object[] args)
        {

            return string.Format(str, args);
        }
        /// <summary>
        ///виджет - "Сообщества" 
        ///Поключать скрипт: script type="text/javascript" src="//vk.com/js/api/openapi.js?117"
        /// </summary>
        /// <param name="targetId">id элемента, который будет являться контейнером для блока сообщества</param>
        /// <param name="groupId">id группы</param>
        /// <param name="width">задает ширину блока в пикселах (целое число > 120). При значении "auto" подстраивается под ширину блока</param>
        /// <param name="height">задает высоту блока в пикселах (целое число от 200 до 1200)</param>
        /// <param name="mode">mode: 0 - отображать участников сообщества. 1 - отображать только название сообщества. 2 - отображать стену сообщества</param>
        /// <param name="wide">wide: 0 - стандартный режим. 1 - при отображении стены этот режим меняет отображение списка постов, добавляя "мне нравится" и фотографию сообщества.</param>
        /// <param name="color">цвет (фон, текст, кнопки)</param>
        /// <returns></returns>
        public static MvcHtmlString VkGroup(this HtmlHelper helper, string containerId, int groupId,
            int width = 200, int height = 200, int mode = 0, int wide = 0, ColorVk color = default(ColorVk))
        {
            containerId = helper.Encode(containerId);

            TagBuilder script = new TagBuilder("script");
            script.MergeAttribute("type", "text/javascript");
            string config = String.Format("wide: '{0}'," +
                                          "mode: '{1}'," +
                                          "width: '{2}'," +
                                          "height: '{3}'," +
                                          "color1: '{4}'," +
                                          "color2: '{5}'," +
                                          "color3: '{6}'", wide.ToString(), mode.ToString(), width.ToString(), height.ToString(),
                                          color.ColorWidget, color.ColorText, color.ColorButton);
            script.InnerHtml = string.Format("{0}Group('{1}',{{ {2} }}, {3})", _widgetVk, containerId, config, groupId.ToString());
            return new MvcHtmlString(script.ToString());
        }
        /// <summary>
        ///  виджет - "Мне нравится" 
        ///  Поключать скрипт: <script type="text/javascript" src="//vk.com/js/api/openapi.js?117"></script>
        /// </summary>
        /// <param name="containerId">id элемента, который будет являться контейнером для блока Like. В случае, если на странице присутствуют несколько блоков "Мне нравится", необходимо использовать разные id для каждого виджета</param>
        /// <param name="width">ширину блока в пикселах (целое число > 200, значение по умолчанию - 350). Параметр учитывается только для кнопки с текстовым счетчиком (type = full)</param>
        /// <param name="height">задает высоту кнопки в пикселах. Допустимые значения 18, 20, 22, 24. Значение по умолчанию - 22.</param>
        /// <param name="page_id">Идентификатор страницы на Вашем сайте. Целое 32хразрядное число. Используется в том случае, если у одной и той же статьи может быть несколько адресов, а также на динамических сайтах, у которых меняется только хеш. Значение по умолчанию - контрольная сумма от location.href</param>
        /// <param name="type">задает вариант дизайна кнопки. Допустимые значения: full (кнопка с текстовым счётчиком), button (кнопка с миниатюрным счётчиком), mini (миниатюрная кнопка), vertical (миниатюрная кнопка, счётчик сверху). Значение по умолчанию - full.</param>
        /// <param name="pageTitle">задает название страницы (для отображения в предпросмотре у записи на стене)</param>
        /// <param name="pageDescription">задает описание страницы (для отображения в предпросмотре у записи на стене)</param>
        /// <param name="pageUrl"> задает адрес страницы (для отображения у записи на стене). Указывайте в том случае, если адрес статьи отличается от адреса, на котором отображается кнопка "Мне нравится"</param>
        /// <param name="pageImage">задает адрес картинки-миниатюры (для отображения в предпросмотре у записи на стене)</param>
        /// <param name="text">задает текст, который будет опубликован на стене в результате нажатия "Рассказать друзьям". Максимальная длина - 140 символов. Значение по умолчанию соответствует названию страницы</param>
        /// <param name="verb">задает вариант формулировки текста внутри кнопки. 1 - Это интересно, 0 - Мне нравится. Значение по умолчанию - 0</param>
        /// <returns></returns>
        public static MvcHtmlString VkLike(this HtmlHelper helper, string containerId, int apiId,
            int width = 200, int height = 200, int? page_id = null, string type = null, string pageTitle = null,
            string pageDescription = null, string pageUrl = null, string pageImage = null, string text = null, int? verb = null)
        {
            containerId = helper.Encode(containerId);


            TagBuilder script = new TagBuilder("script");
            script.MergeAttribute("type", "text/javascript");

            script.InnerHtml += "VK.init({{apiId: {0}, onlyWidgets: true}}); ".StringModernize(apiId.ToString());

            var parametrs = MethodBase.GetCurrentMethod().GetParameters();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < parametrs.Length; i++)
            {
                if (parametrs.GetValue(i) != null)
                {
                    builder.Append(parametrs[i].Name + ":" + (parametrs[i].GetType() is string ? "'"
                        + parametrs.GetValue(i).ToString() + "'" : parametrs.GetValue(i).ToString()));
                    builder.Append(",");
                }
            }

            string config = String.Format("page_id: '{0}'," +
                                          "type: '{1}'," +
                                          "width: '{2}'," +
                                          "height: '{3}'," +
                                          "pageTitle: '{4}'," +
                                          "pageDescription: '{5}'," +
                                          "pageUrl: '{6}'," +
                                          "pageImage: '{7}'," +
                                          "text: '{8}'," +
                                          "verb: '{9}'",
                                          page_id.ToString(), type, width.ToString(), height.ToString(),
                                          pageTitle, pageDescription, pageUrl, pageImage, text, verb.ToString());

            script.InnerHtml += string.Format("{0}Like('{1}',{{ {2} }}, {3})", _widgetVk, containerId, config, page_id ?? 0);
            return new MvcHtmlString(script.ToString());
        }
    }


}