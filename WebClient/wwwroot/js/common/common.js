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
        console.log(textStatus);
        successCallback(result);
    }).fail(function (result) {
        console.log(result.status);
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
        crossDomain : true,
        xhrFields: {
            withCredentials: true,
        },  
        beforeSend: showLoadingPage
    }).done(function (result, textStatus) {
        console.log(result.status);
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
