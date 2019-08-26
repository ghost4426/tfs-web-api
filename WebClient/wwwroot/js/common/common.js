var ajaxCallParams = {};
var ajaxDataParams = {};

var table_vi_lang = {
    "decimal": "",
    "emptyTable": "Không có thực phẩm",
    "info": "Hiển thị _START_ - _END_ trong tổng số _TOTAL_ thực phẩm",
    "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 thực phẩm",
    "infoFiltered": "(Lọc từ _MAX_ tổng thực phẩm)",
    "infoPostFix": "",
    "thousands": ",",
    "loadingRecords": "Đang tải...",
    "processing": "Đang tiến hành...",
    "sLengthMenu": "Hiển thị _MENU_ thực phẩm",
    "search": "Tìm kiếm:",
    "zeroRecords": "Không tìm thấy thực phẩm phù hợp",
    "paginate": {
        "first": "Đầu",
        "last": "Cuối",
        "next": "Sau",
        "previous": "Trước"
    },
    "aria": {
        "sortAscending": ": Lọc từ thấp đến cao",
        "sortDescending": ": Lọc từ cao đến thấp"
    }
};

var userTable_vi_lang = {
    "decimal": "",
    "emptyTable": "Không có người dùng",
    "info": "Hiển thị _START_ - _END_ trong tổng số _TOTAL_ người dùng",
    "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 người dùng",
    "infoFiltered": "(Lọc từ _MAX_ tổng người dùng)",
    "infoPostFix": "",
    "thousands": ",",
    "loadingRecords": "Đang tải...",
    "processing": "Đang tiến hành...",
    "sLengthMenu": "Hiển thị _MENU_ người dùng",
    "search": "Tìm kiếm:",
    "zeroRecords": "Không tìm thấy người dùng phù hợp",
    "paginate": {
        "first": "Đầu",
        "last": "Cuối",
        "next": "Sau",
        "previous": "Trước"
    },
    "aria": {
        "sortAscending": ": Lọc từ thấp đến cao",
        "sortDescending": ": Lọc từ cao đến thấp"
    }
};
var registerInfoTable_vi_lang = {
    "decimal": "",
    "emptyTable": "Không có thông tin cơ sở đăng ký",
    "info": "Hiển thị _START_ - _END_ trong tổng số _TOTAL_ cơ sở",
    "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 cơ sở",
    "infoFiltered": "(Lọc từ _MAX_ tổng cơ sở)",
    "infoPostFix": "",
    "thousands": ",",
    "loadingRecords": "Đang tải...",
    "processing": "Đang tiến hành...",
    "sLengthMenu": "Hiển thị _MENU_ cơ sở",
    "search": "Tìm kiếm:",
    "zeroRecords": "Không tìm thấy cơ sở phù hợp",
    "paginate": {
        "first": "Đầu",
        "last": "Cuối",
        "next": "Sau",
        "previous": "Trước"
    },
    "aria": {
        "sortAscending": ": Lọc từ thấp đến cao",
        "sortDescending": ": Lọc từ cao đến thấp"
    }
};




function callAjaxAuth(callParams, dataParams, successCallback, errorCallback) {
    $.ajax({
        url: callParams.url,
        dataType: callParams.dataType,
        type: callParams.type,
        data: dataParams,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',
            "Authorization": 'Bearer ' + Cookies.get('token')
        },
        crossDomain: true,
        xhrFields: {
            withCredentials: true,
        },
        beforeSend: showLoadingPage,
        statusCode: {
            401: function () {
                window.location.replace("/dang-nhap");
            },
        },
    }).done(function (result, textStatus) {
        successCallback(result);
    }).fail(function (result) {
        errorCallback(result);
    }).always(hideLoadingPage)
};

function callAjax(callParams, dataParams, successCallback, errorCallback) {
    $.ajax({
        url: callParams.url,
        dataType: callParams.dataType,
        type: callParams.type,
        data: dataParams,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',

        },
        crossDomain: true,
        xhrFields: {
            withCredentials: true,
        },
        beforeSend: showLoadingPage
    }).done(function (result) {
        successCallback(result);
    }).fail(function (result) {
        errorCallback(result);
    }).always(hideLoadingPage)
};

function showLoadingPage() {
    $.blockUI({
        message: '<img src="/app-assets/images/icons/busy.gif" style="height:50px" />', css: {
            top: '40%',
            left: '45%',
            border: 'none',
            padding: '15px',
            width: '100px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    })
}

function hideLoadingPage() {
    $.unblockUI();
}


function Authorization() {
    const token = Cookies.get('token');
    if (token == null || token == 'undefined') {
        window.location.replace("/dang-nhap");
    };
    const loggerData = jwt_decode(token);
    var roles = loggerData.role;
    $.each(roles, function (data, value) {
        switch (value) {
            case "Farm":
                $("#main-menu").append(' <ul class="nav navbar-nav" id="main-menu-navigation" data-menu="menu-navigation"> <li class="dropdown nav-item">'
                    + ' <a class= "nav-link" asp-controller="Farm" asp-action="Index" >'
                    + '<i class="fa fa-pied-piper-alt"></i>'
                    + ' <span data-i18n="nav.category.general">Quản lý thực phẩm</span>'
                    + ' </a >'
                    + ' </li >'
                    + '<li class="dropdown nav-item">'
                    + ' <a class="nav-link" asp-controller="Farm" asp-action="Transaction" >'
                    + '<i class="fa fa-pied-piper-alt"></i>'
                    + ' <span data-i18n="nav.category.general">Quản lý thực phẩm</span>'
                    + ' </a >'
                    + ' </li >  </ul>');
                break;
            case "Manager":
                break;
        }
    });
}

jQuery.validator.addMethod("lettersonly", function (value, element) {
    return this.optional(element) || /^[a-zA-Z\s\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+$/i.test(value);
}, "Letters only please"); 

jQuery.validator.addMethod("username", function (value, element) {
    return this.optional(element) || /^[0-9a-zA-Z]+$/i.test(value);
}, "Letters only please"); 

jQuery.validator.addMethod("smallerThan",
    function (value, element, params) {
        if (!/Invalid|NaN/.test(new Date(value))) {
            var today = new Date();
            var ptoday = today.getFullYear();
            if ((today.getMonth() + 1).toString().length == 1) {
                ptoday += "-0" + (today.getMonth() + 1);
            } else {
                ptoday += "-" + (today.getMonth() + 1);
            }
            if (today.getDate().toString().length == 1) {
                ptoday += "-0" + today.getDate();
            } else {
                ptoday += "-" + today.getDate();
            }
            return new Date(value) <= new Date(ptoday);
        }
    }, 'Must be smaller than {0}.');

jQuery.validator.addMethod("greaterThan",
    function (value, element, params) {
        if (!/Invalid|NaN/.test(new Date(value))) {
            return new Date(value) > new Date(params);
        }
    }, 'Must be greaterThan than {0}.');

jQuery.validator.addMethod("checkTreatment",
    function (value, element, params) {
        return params != "";
    }, 'This field is required.');

jQuery.validator.addMethod("checkPackaging",
    function (value, element, params) {
        return params != "";
    }, 'This field is required.');

jQuery.validator.addMethod("checkEXPDate",
    function (value, element, params) {
        if (!/Invalid|NaN/.test(new Date(params))) {
            return new Date(params) > new Date();
        }
    }, 'expired date');

jQuery.validator.addMethod("checkConfirmPass",
    function (value, element, params) {
        if (value == params) return true;
        else return false;
    }, 'confirm not correct');

jQuery.validator.addMethod("numberonly", function (value, element) {
    return this.optional(element) || /^[0-9]+$/i.test(value);
}, "Number only please"); 

var requiredError = "Vui lòng nhập trường này";
var letterOnlyError = "Vui lòng chỉ nhập chữ (Không có ký tự đặc biệt)";
var userError = "Vui lòng không nhập ký tự đặc biệt";
var emailError = "Vui lòng nhập đúng email (vidu@gmail.com.vn)";
var numberOnlyError = "Vui lòng chỉ nhập số";

function containsObject(obj, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        }
    }

    return false;
}
