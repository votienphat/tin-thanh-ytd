/**
Định nghĩa format cho ngày tháng
**/
var DateTimeFormat = {
    UpperDate: "DD/MM/YYYY",
    UpperDateTime: "DD/MM/YYYY HH:mm:ss",
    UpperMonth: "MM/YYYY",
    UpperHour: "HH",
    UpperDateYYYYMMDD: "YYYY/MM/DD",
    UpperDateYYYY: "YYYY",

    UpperDateEN: "MM/DD/YYYY",
    UpperDateTimeEN: "MM/DD/YYYY HH:mm:ss",

    LowerMonth: "mm/yyyy",
    LowerDate: "dd/mm/yyyy",
    LowerDateTime: "dd/mm/yyyy hh:mm:ss",
    LowerTimeFull: "dd/mm/yyyy hh:ii:ss",

}
var DataTablesConst = {
    LengthMenu: [
        [10, 20, 50, 100],
        [10, 20, 50, 100] // change per page values here
    ],
    PageLength: 10,
    Language: {
        "lengthMenu": " _MENU_ dòng"
    }
};

/************* jQuery Extend Start *************/
//jQuery.fn.extend();
/************* jQuery Extend Start *************/

/************* Toastr Start *************/
var showSuccessToastr = function (msg, title, callback) {
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['success'](msg, title);
}

var showInfoToastr = function (msg, title, callback) {
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['info'](msg, title);
}

var showWarningToastr = function (msg, title, callback) {
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['warning'](msg, title);
}

var showErrorToastr = function (msg, title, callback) {
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['error'](msg, title);
}
/************* Toastr End *************/

/************* Loading Start *************/

var ShowLoading = function () {
    Metronic.blockUI({
        animate: true
    });
}
var HideLoading = function () {
    Metronic.unblockUI();
}

/************* Loading End *************/

/************* PopUp Start *************/
function OpenPopup(title, url, width, callback) {
    if (typeof width === "undefined" || width === null) {
        width = 600;
    }
    $("#tinyPopupLabel").html(title);
    $("#tinyPopup .modal-dialog").css("width", width + "px");
    ShowLoading();
    $.get(url, function (data) {
        $("#tinyPopupBody").html(data);
        HideLoading();
        $("#tinyPopup").modal({ show: true, backdrop: false });
        if (typeof callback == "function")
            callback();

    }).fail(function (xhr, status, error) {
        HideLoading();
    });
}
function ClosePopup() {
    $("#tinyPopup").modal("hide");

}
/************* PopUp End *************/

function convertNameRewrite(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
    /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
    str = str.replace(/^\-+|\-+$/g, "");
    return str;
}

function checkMail(str) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(str);
};
function ConvertToNumber(currency) {
    var num = currency.toString().replace(/\VND|\,|\s/g, '').replace(/\VNĐ|\,|\s/g, '');
    return parseInt(num);
}
function formatMoney(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    return x1 + x2;
}

/************* Xử lý quyền Start *************/

function checkRight(json) {
    if (json.Status != 'undefined') {
        switch (json.Status) {
            case 1000:
                showErrorToastr('Tài khoản của bạn đã hết hạn. Bạn cần đăng nhập lại.');
                PopupLoginShow();
                return false;
            case 1001:
                showErrorToastr('Bạn không có quyền thực hiện thao tác này.');
                return false;
        }
    }
    return true;
}

/************* Xử lý quyền End *************/


/************* Xử lý popup login *************/

function PopupLoginShow() {
    $("#LoginPopup").modal("show");
}
var CheckLogin = function () {
    var allow = true;
    var username = '' + $("#LoginPopup .modal-body .form-group #Username").val();
    var pass = '' + $("#LoginPopup .modal-body .form-group #Password").val();
    if (username == '') {
        allow = false;
    }
    if (pass == '') {
        allow = false;
    }
    if (allow) {
        Loading.Show();
        $.ajax({
            type: "POST",
            url: '/../../Account/LoginPopup', // ApiUrl đã được cấu hình sẵn
            data: { "UserName": username, "Password": pass },
            dataType: "json",
            success: function (response) {
                if (response.Code == 1) {
                    $("#LoginPopup").modal("hide");
                    showSuccessToastr("Đăng nhập thành công");
                } else {
                    showErrorToastr("Đăng nhập thất bại");
                }
            },
            complete: function () {
                Loading.Hide();
            },
            error: function (e) {
                showErrorToastr("Đăng nhập thất bại");
            }
        });
    }
}
/************* Xử lý popup login  *************/
