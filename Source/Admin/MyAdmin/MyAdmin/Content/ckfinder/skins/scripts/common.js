//=============== Hien thi Thong bao ===================
var hx = 0;
var timeout_id;
var iSubFolder;
var ans_confirm = false;
function showErrorRegisters(status, strMess) {
    var html = "";

    html = '<div class="notification ' + (status ? 'success' : 'error') + ' png_bg">' +
            '<div>' +
                strMess +
            '</div>' +
        '</div>';

    $('#msgError').html(html);
    top.scroll(0, 0);
    try { showPopupErrors(); } catch (e) { alert(e); }
}
function showPopupErrors() {
    $('#msgError').slideDown("slow");
    setTimeout("hidePopupErrors()", 4000);
}
function hidePopupErrors() {
    $('#msgError').slideUp();
}
function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

//TanPVD: 10/03/2015
function IsNumber($input) {
    var reg = /^[0-9]+$/;
    if (!reg.test($input)) {
        return false;
    } else {
        return true;
    }
}

//upper Case first character
function ucfirst(str) {
    var strReturn = "";
    if (str != null) {
        var arrStr = str.split(" ");

        var firstLetter = "";
        for (var i = 0; i < arrStr.length; i++) {
            firstLetter = arrStr[i].substr(0, 1);
            strReturn += firstLetter.toUpperCase() + arrStr[i].substr(1).toLowerCase();
            if (i < arrStr.length - 1)
                strReturn += " ";
        }
    }

    return strReturn;
}
function checkMail(str) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(str);
};

function inputNum(el) {
    el.keydown(function (e) {
        var key = e.charCode || e.keyCode || 0;

        // allow enter, backspace, tab, delete, arrows, numbers and keypad numbers ONLY
        return (
     key == 13 ||
     key == 8 ||
     key == 9 ||
     key == 46 ||
    key == 109 ||
    key == 173 ||
     (key >= 37 && key <= 40) ||
     (key >= 48 && key <= 57) ||
     (key >= 96 && key <= 105));
    });
    el.blur(function () {
        if ($(this).val().trim() == "")
            $(this).val("0");
    });
}
$.fn.pressEnter = function (fnc) {
    return this.each(function () {
        $(this).keypress(function (ev) {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == '13') {
                fnc.call(this, ev);
                return false;
            }
        })
    })
}

function ValidateIPAddress(inputText) {
    var ipformat = new RegExp(/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/);
    return ipformat.test(inputText);
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

function ConvertToNumber(currency) {
    var num = currency.toString().replace(/\VND|\,|\s/g, '').replace(/\VNĐ|\,|\s/g, '');
    return parseInt(num);
}

function ConvertToCurrency(num) {
    num = num.toString().replace(/,/g, '');
    if (isNaN(num))
        num = "0";
    var sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    //cents = num % 100;
    num = Math.floor(num / 100).toString();
    //if (cents < 10)
    //cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
    num.substring(num.length - (4 * i + 3));
    return num;
}
/*function convertNameRewrite(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
    /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - #1#
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
    str = str.replace(/^\-+|\-+$/g, "");
    return str;
}*/

/*popup alert*/

function Confirm_popup(title, strContent, callback) {
        if ($("#popupConfirm-screen").length == 0) {
            $("body").append('<div class="ui-popup-screen ui-overlay-b in" id="popupConfirm-screen"></div>');
        }
        if ($("#popupDialog-popup").length == 0) {
            $("body").append('<div class="ui-popup-container pop in ui-popup-active" id="popupDialog-popup" tabindex="0" ></div>');
        }
        var strHTML = '<div style="max-width: 400px;" class="ui-popup ui-body-b ui-overlay-shadow ui-corner-all">' +
            '<div class="ui-header ui-bar-a">' +
            '<h1 class="ui-title" >' + title + '</h1>' +
            '</div>' +
            '<div role="main" class="ui-content">' +
            '<h3 class="ui-title">' + strContent + '</h3>' +
            '<a href="javascript:void(0);" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-btn-b"><strong>Cancel</strong></a>' +
            '<a href="javascript:void(0);" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-btn-b"><strong>Ok</strong></a>' +
            '</div>' +
            '</div>';

        $("#popupDialog-popup").html(strHTML);
        var widthScreen = $("body").width();
    var heightScreen = $("body").height();
    $("#popupConfirm-screen").css({"width":widthScreen +"px","height": heightScreen +"px"});
        $("#popupConfirm-screen").unbind("click").click(function() {
            positionPopup($("#popupDialog-popup"));
        });
        $(document).resize(function() {
            positionPopup($("#popupDialog-popup"));
            var widthScreen = $("body").width();
            var heightScreen = $("body").height();
            $("#popupConfirm-screen").css({ "width": widthScreen + "px", "height": heightScreen + "px" });
        });
        positionPopup($("#popupDialog-popup"));
        $("#popupConfirm-screen").show();
        $("#popupDialog-popup").slideDown(500);
    $("#popupDialog-popup a").click(function () {
            $("#popupDialog-popup").slideUp();
            $("#popupConfirm-screen").hide();
        if ($(this).text() == "Ok") {
                callback();
            }
        });
}

function positionPopup(elemntPopup) {
    var positionHeight = ($(window).height() - elemntPopup.outerHeight()) / 2;
    var positionWidth = ($(window).width() - elemntPopup.outerWidth()) / 2;
    elemntPopup.css({ 'position': 'fixed', "top": positionHeight + "px", "left": positionWidth + "px" });
}
/*end popup alert*/

function convertTinyCurrency(intValue) {
    var strReturn = 0;
    if (intValue >= 1000000000) {
        strReturn = formatMoney(Math.round(intValue / 1000000000)) + " B";
    } else if (intValue >= 1000000) {
        strReturn = formatMoney(Math.round(intValue / 1000000)) + " M";
    }else if (intValue >= 1000) {
        strReturn = formatMoney(Math.round(intValue / 1000)) + " K";
    } else {
        strReturn = formatMoney(intValue);
    }
    return strReturn;
}

/*Begin Popup Bootstrap*/
var funOk, funCancle;
var popup = '<div class="modal fade" id="myPopup" tabindex="-1" role="dialog" aria-labelledby="myPopupLabel" aria-hidden="true">';
popup += '<div class="modal-dialog">';
popup += '        <div class="modal-content">';
popup += '            <div class="modal-header">';
popup += '                <button type="button" class="close" data-dismiss="modal">';
popup += '                    <span aria-hidden="true">x</span>';
popup += '                </button>';
popup += '                <h4 class="modal-title" id="myPopupLabel">Modal title</h4>';
popup += '            </div>';
popup += '            <div class="modal-body">';
popup += '                nội dung';
popup += '            </div>';
popup += '            <div class="modal-footer">';
popup += '                <button id="btnCancle" type="button" class="btn btn-default" data-dismiss="modal">Hủy bỏ</button>';
popup += '                <button id="btnOk" type="button" class="btn btn-primary" >Đồng ý</button>';
popup += '            </div>';
popup += '        </div>';
popup += '    </div>';
popup += '</div>';

$(document).ready(function () {
    $(document).on("click", "#btnCancle", function () {
        CloseModal();
        if (typeof funCancle == 'function') {
            funCancle.call(this);
        }
    });
    //$(document).on("click", "#btnOk", function () {
    //    CloseModal();
    //    if (typeof funOk == 'function') {
    //        funOk.call(this);
    //    }
    //});

    $('.list-bang .css-label').popover({ html: true });
});

function CallPopup(title, body, status) {
    if ($("#myPopup").length == 0)
        $("body").prepend(popup);
    $("#myPopup #myPopupLabel").html(title);
    if (!status) {
        $("#myPopup .modal-body").addClass("Error");
    } else {
        $("#myPopup .modal-body").removeClass("Error");
    }
    $("#myPopup .modal-body").html(body);
    $("#btnCancle").hide();
    $("#btnOk").hide();
    $("#myPopup .modal-footer").hide();
    $("#myPopup").modal('show');
}
function CallPopupOk(title, body, okCallback) {
    if ($("#myPopup").length == 0)
        $("body").prepend(popup);
    //funOk = okCallback;
    $("#myPopup #myPopupLabel").html(title);
    $("#myPopup .modal-body").html(body);
    $("#btnCancle").hide();
    $("#btnOk").show();
    $("#myPopup").modal('show');
    $("#btnOk").unbind("click").click(function () {
        okCallback();
    });
}
function CallPopupOkCancel(title, body, okCallback, cancleCallback) {
    if ($("#myPopup").length == 0)
        $("body").prepend(popup);
    //funOk = okCallback;
    //funCancle = cancleCallback;
    $("#myPopup #myPopupLabel").html(title);
    $("#myPopup .modal-body").html(body);
    $("#myPopup .modal-footer").show();
    $("#btnCancle").show();
    $("#btnOk").show();
    $("#myPopup").modal('show');
    
    $("#btnOk").unbind("click").click(function () {
        okCallback();
        CloseModal();
    });
    $("#btnCancle").unbind("click").click(function () {
        cancleCallback();
        CloseModal();
    });
}


function CloseModal() {
    $("#myPopup").remove();
    //$(".modal-footer button[data-dismiss].close").click();
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}
/*End popup BootStrap*/

loading = {
    Show: function (notify) {
        if (notify != null)
            $("#ajax-loading label").html(notify + " - Nhấp double chuột để đóng.");
        else
            $("#ajax-loading label").html("Nhấp double chuột để đóng.");
        $("#ajax-loading").removeClass("hidden");
    },
    Hide: function () {
        $("#ajax-loading").addClass("hidden");
    }
};

var Confirm = {
    YesNo: function(title, content, yesText, noText, width, yesCallback, noCalback) {
        var footer = '<div class="modal-footer text-right">';
        footer += '          <button id="btnConfirmYes" data-dismiss="modal" class="btn btn-primary" type="button">' + yesText + '</button>';
        footer += '          <button data-dismiss="modal" class="btn btn-danger" type="button">' + noText + '</button>';
        footer += '     </div>';
        $("#modalConfirmLabel").html(title);
        $("#modalConfirm .modal-body").html(content + footer);
        $("#modalConfirm  .modal-dialog").css("width", width + "px");
        $("#modalConfirm").modal("show");

        $("#btnConfirmYes").click(function() {
            if (typeof yesCallback == 'function') {
                yesCallback();
            }
        });
        $('#modalConfirm').on('hidden.bs.modal', function(e) {
            if (typeof noCalback == 'function') {
                noCalback();
            }
        });

    }
};

var Popup = {
    EventClose: null,
    Show: function (title, content, width, callbackhiden) {
        $("#modalPopupLabel").html(title);
        $("#modalPopup .modal-body").html(content);
        $("#modalPopup  .modal-dialog").css("width", width + "px");
        Popup.EventClose = callbackhiden;
        $("#modalPopup").modal("show");
    },
    Hide: function (callback) {
        $("#modalPopup").modal("hide");
        $("#modalPopup .modal-body").html("");
        if (typeof callback == 'function') {
            callback();
        }
    },
};


var Alert = {
    Success: function(title, content, width, callbackhiden) {
        $("#modalPopup").addClass("confirm");
        content = "<div class='success-icon'>" + content + "</div>";
        Popup.Show(title, content, width, callbackhiden);
    },
    Error: function(title, content, width, callbackhiden) {
        $("#modalPopup").addClass("confirm");
        content = "<div class='error-icon'>" + content + "</div>";
        Popup.Show(title, content, width, callbackhiden);
    },
    Warning: function(title, content, width, callbackhiden) {
        $("#modalPopup").addClass("confirm");
        content = "<div class='warning-icon'>" + content + "</div>";
        Popup.Show(title, content, width, callbackhiden);
    }
};

Loading = {
    Opacity: function () {
        $("#loading").html("");
        $("#loading").show();
    },
    Show: function (title) {
        var winHeight = $(window).height();
        img = '<div class="loading-img"><img src="/Content/Theme/Default/img/loading2.gif"></div>';
        $("#loading").html(img);
        $(".loading-img").css("margin-top", winHeight / 2 - 100 + "px");
        $("#loading").show();
    },
    Hide: function () {
        $("#loading").hide();
    },
};

function HtmlDecode(s) {
    return $('<div>').html(s).text();
}

var capitalizeMe = function (obj) {
    var val = obj;
    var newVal = '';
    var arrVal = val.split(' ');
    for (var c = 0; c < arrVal.length; c++) {
        newVal += arrVal[c].substring(0, 1).toUpperCase() +
            arrVal[c].substring(1, arrVal[c].length);
        newVal += (arrVal.length > 1 ? " " : "");
    }
    return newVal;
};

var _idflash = 0;
var CreateImageOrFlash = function (imgPath, alt) {
    _idflash++;
    if (imgPath.length >= 4) {
        var ex = imgPath.substr(imgPath.length - 4);
        if (ex == '.swf' || imgPath.indexOf('.swf') > 0) {
            var str = '<object data="' + imgPath + '" id="client_' + _idflash + '">';
            str += '           <param name="wmode" value="transparent">';
            str += '                <param value="true" name="allowFullScreen">';
            str += '                <param value="always" name="allowScriptAccess">';
            str += '                <param value="all" name="AllowNetworking">';
            str += '            </object>';

            return str;
        }
        else {
            return '<img alt="' + alt + '" src="' + imgPath + '"/>';
        }
    } else {
        return '<img alt="' + alt + '" src="' + imgPath + '"/>';
    }
}

var CreatePaging = function (pageIndex, totalItem, itemInPage, countDisplayPage, fnName) {
    if (pageIndex <= 0 || totalItem <= 0 || itemInPage <= 1)
        return "";
    if (countDisplayPage == null || countDisplayPage == "" || countDisplayPage == "undefined")
        countDisplayPage = 5;
    var totalPage = Math.ceil(totalItem / itemInPage);
    if (totalPage <= 1)
        return "";
    //tinh so trang show;
    var cdp = Math.floor(countDisplayPage / 2);
    var startpage = pageIndex - cdp;
    if (startpage < 1)
        startpage = 1;
    var endpage = startpage + cdp * 2;
    if (endpage > totalPage)
        endpage = totalPage;

    //console.log("totalPage="+totalPage);
    //console.log("startpage="+startpage);
    //console.log("endpage=" + endpage);
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