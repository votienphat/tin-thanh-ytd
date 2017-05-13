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
    //CloseAllPopup();
    //Loading.Show();
    $.ajax({
        url: url,
        cache: false
    })
.done(function (html) {
    if ($.trim(html).length > 0) {
        //Loading.Hide();
        PopupFull.Show(title, html, callbackhidden);
    } else {
        //Loading.Hide();
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
