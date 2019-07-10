var ajaxCallParams = {};
var ajaxDataParams = {};

var table_vi_lang = {
    "decimal": "",
    "emptyTable": "Không có giá trị",
    "info": "Hiển thị _START_ đến _END_ trong tổng số _TOTAL_ sản phẩm",
    "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 sản phẩm",
    "infoFiltered": "(Lọc từ _MAX_ tổng sản phẩm)",
    "infoPostFix": "",
    "thousands": ",",
    "loadingRecords": "Loading...",
    "processing": "Đang tiến hành...",
    "search": "Tìm kiếm:",
    "zeroRecords": "Không tìm thấy sản phẩm phù hợp",
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
            "Authorization": 'Bearer ' + sessionStorage.getItem('token')
        },
        beforeSend: startLoadingPage
    }).done(function (result) {
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
            'Content-Type': 'application/json; charset=utf-8'
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
