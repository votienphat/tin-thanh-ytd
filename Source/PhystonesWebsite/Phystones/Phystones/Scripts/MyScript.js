var PopupFull = {
    EventClose: function (closeFunction) {
        $("#modalPopupFull .close").click(function () {
            if (typeof closeFunction === 'function') {
                closeFunction();
            }
        });
    },
    Show: function (title,content, callbackhidden) {
        $("#modalPopupFull .modal-body").html(content);
        $("#modalPopupFull .modal-title").html(title);
        $("#modalPopupFull .close").click(function () {
            if (typeof callbackhidden === 'function') {
                callbackhidden();
            }
        });
        $("#modalPopupFull").modal("show");
    },
    Hide: function (callback) {
        $("#modalPopupFull").modal("hide");
        $("#modalPopupFull .modal-body").html("");
        if (typeof callback === 'function') {
            callback();
        }
    }
};
var OpenPopupFull = function (url, title, callbackhidden) {
    $.ajax({
        url: url,
        cache: false
    })
.done(function (html) {
    if ($.trim(html).length > 0) {
        PopupFull.Show(title, html, callbackhidden);
    } else {
        Popup.Show("Thông Báo", "Vui lòng tải lại trang");
    }
});
};
var PopupPortfolio = {
    EventClose: function (closeFunction) {
        $("#modalPortfolio #modal-close").click(function () {
            if (typeof closeFunction === 'function') {
                closeFunction();
            }
        });
    },
    Show: function (content, callbackhidden) {
        $("#modalPortfolio .modal-content").html(content);
        $("#modalPortfolio #modal-close").click(function () {
            if (typeof callbackhidden === 'function') {
                callbackhidden();
            }
        });
        $("#modalPortfolio").modal("show");
    },
    Hide: function (callback) {
        $("#modalPortfolio").modal("hide");
        $("#modalPortfolio").html("");
        if (typeof callback === 'function') {
            callback();
        }
    }
};
var OpenPopupPortfolio = function (index) {
    $.ajax({
        url: '/popupPortfolio',
        cache: false,
        data: { Id: index }
    })
        .done(function (html) {
            if ($.trim(html).length > 0) {
                PopupPortfolio.Show(html);
            } else {
                Loading.Hide();
            }
        });
};
var CreatePaging = function (pageIndex, totalItem, itemInPage, countDisplayPage, fnName) {
    if (pageIndex <= 0 || totalItem <= 0 || itemInPage <= 0) {
        return "";
    }
    if (countDisplayPage == null || countDisplayPage == "" || countDisplayPage == "undefined")
        countDisplayPage = 5;
    var totalPage = Math.ceil(totalItem / itemInPage);
    if (totalPage <= 1)
        return "";
    var cdp = Math.floor(countDisplayPage / 2);
    var startpage = pageIndex - cdp;
    if (startpage < 1)
        startpage = 1;
    var endpage = startpage + cdp * 2;
    if (endpage > totalPage)
        endpage = totalPage;
    var ul = '<ul class="pagination pagination-sm">';
    if (pageIndex > 1) {
        var onclickF = "";
        if (fnName != null && fnName.length > 0)
            onclickF = fnName + "(" + (pageIndex - 1) + ")";
        ul += '<li><a onclick="' + onclickF + '" datapage="' + (pageIndex - 1) + '" href="javascript:void(0)" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>';
    }
    for (var i = startpage; i <= endpage; i++) {
        var active = "";
        if (i == pageIndex)
            active = "active";
        var onclick = "";
        if (fnName != null && fnName.length > 0)
            onclick = fnName + "(" + i + ")";
        var li = '<li class="' + active + '"><a  onclick="' + onclick + '" datapage="' + i + '"  href="javascript:void(0)">' + i + '</a></li>';
        ul += li;
    }
    if (pageIndex < totalPage) {
        var onclickL = "";
        if (fnName != null && fnName.length > 0)
            onclickL = fnName + "(" + (pageIndex + 1) + ")";
        ul += '<li><a onclick="' + onclickL + '" datapage="' + (pageIndex + 1) + '" href="javascript:void(0)" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li>';
    }
    ul += "</ul>";
    return ul;
};
var Loading = {
    Show: function () {
        $("#loading").show();
    },
    Hide: function () {
        $("#loading").hide();
    }
};