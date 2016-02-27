//.loading {

//    width: 100%;
//    height: 100%;
//    position: absolute;
//}
//<div style="position:relative;">
//    <div id="loading" class="loading" style="display: none">
//        <div style="display: table; width: 100%; height: 100%;text-align: center">
//            <h2 style="vertical-align: middle; display: table-cell;">Please Wait</h2>
//        </div>
//    </div>
//    <div id="table-cities">
//        @Html.Partial("_ShowCities", @Model)
//    </div>
//</div>
var ModulePaging = {};

ModulePaging.Info = function () {
    this.PagingID = "",
    this.CountButtonShow = 9,
    this.Config = {
        AjaxUrl: "",
        Href: "",
        UpdateId: "",
        LoadElemId: "",
        LeftPrev: "...",
        RightPrev: "...",
        ButtonCssActive: "active",
        ButtonCss: "btn btn-info",
        AjaxMode: "replace",
        IsAsync: "true",
        OnBegin: "",
        OnSuccess: "",
        UseRoute: false
    },
    this.DataObject = {
        page: 1
    }
};
ModulePaging.Events = function () {
    function asyncOnSuccess(element, data, contentType) {
        var mode;

        if (contentType.indexOf("application/x-javascript") !== -1) {  // jQuery already executes JavaScript for us
            return;
        }

        mode = (element.getAttribute("data-ajax-mode") || "").toUpperCase();
        $(element.getAttribute("data-ajax-update")).each(function (i, update) {
            var top;

            switch (mode) {
                case "BEFORE":
                    top = update.firstChild;
                    $("<div />").html(data).contents().each(function () {
                        update.insertBefore(this, top);
                    });
                    break;
                case "AFTER":
                    $("<div />").html(data).contents().each(function () {
                        update.appendChild(this);
                    });
                    break;
                case "REPLACE-WITH":
                    $(update).replaceWith(data);
                    break;
                default:
                    $(update).html(data);
                    break;
            }
        });
    }
    function getFunction(code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }
        if (typeof (fn) === "function") {
            return fn;
        }
        argNames.push(code);
        return Function.constructor.apply(null, argNames);
    }
    function isMethodProxySafe(method) {
        return method === "GET" || method === "POST";
    }
    function asyncOnBeforeSend(xhr, method) {
        if (!isMethodProxySafe(method)) {
            xhr.setRequestHeader("X-HTTP-Method-Override", method);
        }
    }
    function asyncRequest(element, options) {
        var confirm, loading, method, duration;

        confirm = element.getAttribute("data-ajax-confirm");
        if (confirm && !window.confirm(confirm)) {
            return;
        }

        loading = $(element.getAttribute("data-ajax-loading"));
        duration = parseInt(element.getAttribute("data-ajax-loading-duration"), 10) || 0;

        $.extend(options, {
            type: element.getAttribute("data-ajax-method") || undefined,
            url: element.getAttribute("data-ajax-url") || undefined,
            cache: !!element.getAttribute("data-ajax-cache"),
            beforeSend: function (xhr) {
                var result;
                asyncOnBeforeSend(xhr, method);
                result = getFunction(element.getAttribute("data-ajax-begin"), ["xhr"]).apply(element, arguments);
                if (result !== false) {
                    loading.show(duration);
                }
                return result;
            },
            complete: function () {
                loading.hide(duration);
                getFunction(element.getAttribute("data-ajax-complete"), ["xhr", "status"]).apply(element, arguments);
            },
            success: function (data, status, xhr) {
                asyncOnSuccess(element, data, xhr.getResponseHeader("Content-Type") || "text/html");
                getFunction(element.getAttribute("data-ajax-success"), ["data", "status", "xhr"]).apply(element, arguments);
            },
            error: function () {
                getFunction(element.getAttribute("data-ajax-failure"), ["xhr", "status", "error"]).apply(element, arguments);
            }
        });

        
        $.ajax(options);
    }
    this.SetEvent = function (elementId, func) {

        $(elementId).on("click", "a[data-ajax-paging=true]", function (evt) {

            var req = func != undefined ? func() : null;
            evt.preventDefault();
            evt.stopPropagation();
            asyncRequest(this, {
                url: this.href,
                type: "GET",
                data: req
            });
        });
    }
}
ModulePaging.Methods = function (_info) {

    var info = _info;

    this.CreateButtons = function () {

        if ($(info.PagingID).has("a"))
            $(info.PagingID).empty();

        for (var i = 0; i < info.CountButtonShow; i++) {
            $("<a/>").attr({
                "data-ajax-mode": info.Config.AjaxMode,
                "data-ajax-paging": info.Config.IsAsync,
                "data-ajax-update": "#" + info.Config.UpdateId,
                "data-ajax-loading": "#" + info.Config.LoadElemId,
                "data-ajax-begin": info.Config.OnBegin,
                "data-ajax-success": info.Config.OnSuccess
            }).appendTo(info.PagingID);

        }
        $(info.PagingID).children().addClass(info.Config.ButtonCss);
    },
    this.Generate = function () {


        //события
        //с перересовкой содержимых кнопок
        var onSelectedRefresh = function () { };
        //с перересовкой содержимых кнопок
        var onSelectedWithoutRefresh = function () { };

        //создание кнопки
        var createElement = function (elem, page) {

            if (info.Config.AjaxUrl)
            elem.attr("data-ajax-url", info.Config.AjaxUrl(page));
            if (info.Config.Href)
                elem.attr("href", info.Config.Href(page));

            elem.css("visibility", "visible");
            elem.attr("data-current-page", page);
            elem.html(page);

        }
        //роут, заменяет или добавляет в историю браузера путь (меняет url в адресной строке)
        var route = function (url, isReplace) {
            if (info.Config.UseRoute) {

                if (isReplace) {
                    history.replaceState({ 'previous_url': url }, '', url);
                } else {
                    history.pushState({ 'previous_url': url }, '', url);
                }
            }
        }
        //удаление данных с кнопок (текущая страница, кнопки << и >>)
        var clearAttrDataFromButton = function (elems) {
            elems.removeClass(info.Config.ButtonCssActive);
            elems.removeAttr("data-non-action").removeAttr("data-current-page");;
        }
        //удаляет все события кнопок
        var removeAllEventButton = function (elems) {
            elems.off("click");
        }
        //получить все кнопки
        var getAllButtons = function () {
            return $(info.PagingID).children();
        }
        //получить выбранную страницу
        var getCurrentPage = function () {
            return parseInt($("#" + info.Config.InfoDataId).attr("data-current-page"));
        }
        //установить значение текущей страницы
        var setCurrentPage = function (page) {
            $("#" + info.Config.InfoDataId).attr("data-current-page", page);
        }
        //получить кол-во страниц
        var getTotalPage = function () {
            return parseInt($("#" + info.Config.InfoDataId).data("total-pages"));
        };
        //генерация кнопок
        //генерация левой стороны
        var leftButtonGenerate = function (elems, opjForButton) {

            if (opjForButton.numberButton > 2) {
                createElement(elems.eq(opjForButton.currentItem++), 1);

                createElement(elems.eq(opjForButton.currentItem), opjForButton.numberButton - 1);
                elems.eq(opjForButton.currentItem++).html(info.Config.LeftPrev);
            }
        };
        //генерация средних 
        var middleButtonGenerate = function (elems, opjForButton, current, totalPages) {

            for (; opjForButton.numberButton < (current + 3) && opjForButton.numberButton <= totalPages; opjForButton.currentItem++, opjForButton.numberButton++) {

                if (opjForButton.numberButton == current) {
                    var e = elems.eq(opjForButton.currentItem);
                    e.addClass(info.Config.ButtonCssActive);
                    e.attr("data-non-action","");
                    e.prev().attr("data-non-action", "");
                    e.next().attr("data-non-action", "");
                }
                createElement(elems.eq(opjForButton.currentItem), opjForButton.numberButton);
            }
        }
        //генерация правой стороны
        var rightButtonGenerate = function (elems, opjForButton, totalPages) {
            if (opjForButton.numberButton + 2 <= totalPages) {
                createElement(elems.eq(opjForButton.currentItem), opjForButton.numberButton);
                elems.eq(opjForButton.currentItem++).text(info.Config.RightPrev);

                createElement(elems.eq(opjForButton.currentItem++), totalPages);

            } else {

                while (opjForButton.numberButton <= totalPages) {
                    createElement(elems.eq(opjForButton.currentItem++), opjForButton.numberButton++);
                }
            }

        }
        //скрывает кнопки которые не используются
        var hideButtons = function (elems, opjForButton) {
            while (opjForButton.currentItem < elems.size()) {
                elems.eq(opjForButton.currentItem++).css("visibility", "collapse");
            }
        }
        //генерация кнопок
        this.CalculatePaging = function () {

            var totalPages = getTotalPage();
            var currentPage = getCurrentPage();

            var elems = getAllButtons();

            var opjForButton = {
                numberButton: currentPage - 2 > 3 ? currentPage - 2 : 1,
                currentItem: 0
            };

            console.log(totalPages);
            clearAttrDataFromButton(elems);
            leftButtonGenerate(elems, opjForButton);
            middleButtonGenerate(elems, opjForButton, currentPage, totalPages);
            rightButtonGenerate(elems, opjForButton, totalPages);
            hideButtons(elems, opjForButton);

            if (info.Config.Href)
            route(info.Config.Href(currentPage), true);
            removeAllEventButton(elems);
            onSelectedRefresh(elems);
            onSelectedWithoutRefresh(elems);

        }

        //реализация события с перерисовкой кнопок
        var createPaging = this.CalculatePaging;
        var clickRefreshPaging = function (e) {

            var url = $(e.currentTarget).attr("href");
            route(url, false);
            var page = $(e.currentTarget).attr("data-current-page");
            setCurrentPage(page);
            createPaging();
        };
        onSelectedRefresh = function (elems) {

            elems.filter(":not([data-non-action])").click(clickRefreshPaging);
           
        };
        //реализация события без перерисовки кнопок
        onSelectedWithoutRefresh = function (elems) {

            var arr = elems.filter(function () {
                //console.log($._data(this, 'events'));
                var ev = $._data(this, 'events');
                if (ev && ev.click)
                    return false;
                return true;
            });

            arr.on("click", function (e) {
                var url = $(e.currentTarget).attr("href");
                history.pushState({ 'previous_url': url }, '', url);

                $(elems).removeClass(info.Config.ButtonCssActive);
                $(e.currentTarget).addClass(info.Config.ButtonCssActive);

            });
        }

        this.CalculatePaging();
    }
}

//возвращет метод watch() чтобы вызвать перересовку кнопок
$.fn.extend({

    PagingSetup: function (info, func) {

        var id = $(this).attr("id");
        info.PagingID = "#" + id;
       
        var paging = new ModulePaging.Methods(info);
        paging.CreateButtons();
        paging.Generate();
        
        var event = new ModulePaging.Events();
        event.SetEvent(info.PagingID, func);
        return { watch: paging.Generate }

    }
});

