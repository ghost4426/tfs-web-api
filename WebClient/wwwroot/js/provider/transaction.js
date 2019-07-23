var providerTransactionTable = $('#provider-transaction-mng').DataTable({
    ajax: {
        url: GET_PROVIDER_TRANSACTION_URI,
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    ordering: false,
    columns: [
        { data: 'TransactionId' },
        { data: 'FoodName' },
        { data: 'FoodBreed' },
        { data: 'Farm' },
        {
            data: 'CreatedDate',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        { data: 'StatusId', visible: false },
        { data: 'Status', visible: false },
        {
            data: function (data, type, dataToSet) {
                if (data.StatusId == 1) {
                    return "<span class='badge badge-glow badge-pill badge-primary'>" + data.Status + "</span>";
                } else if (data.StatusId == 2) {
                    return "<span class='badge badge-glow badge-pill badge-info'>" + data.Status + "</span>";
                } else if (data.StatusId == 3) {
                    return "<span class='badge badge-glow badge-pill badge-success'>" + data.Status + "</span>";
                } else if (data.StatusId == 4) {
                    return "<span class='badge badge-glow badge-pill badge-danger'>" + data.Status + "</span>";
                }
            }
        },
        { data: 'RejectedReason' },
        {
            data: function (data, type, dataToSet) {
                var btnDeny = "<button class='btn btn-sm btn-danger btn-deny-trans' title='Từ chối giao dịch'><i class='fa fa-times'></i></button> ";
                var btnAccept = "<button class='btn btn-sm btn-success btn-accept-trans' title='Xác nhận giao dịch'><i class='fa fa-check'></i></button> ";
                var btnAcceptDis = "<button class='btn btn-sm btn-success btn-accept-trans' title='Xác nhận giao dịch' disabled><i class='fa fa-check'></i></button> ";
                var btnDenyDis = "<button class='btn btn-sm btn-danger btn-deny-trans' title='Từ chối giao dịch' disabled><i class='fa fa-times'></i></button> ";
                var btnBarcode = '<button class="btn btn-secondary btn-sm btn-barcode" title="Barcode"><i class="fa fa-barcode"></i></button> '
                if (data.StatusId == 2) {
                    return btnDeny + btnAccept + btnBarcode;
                } else if (data.StatusId == 4 || data.StatusId == 3) {
                    return btnDenyDis + btnAcceptDis + btnBarcode;
                }
                else return btnDeny + btnAcceptDis + btnBarcode;
            }
        }
    ],
    dom: '<"row" <"col-sm-12"Bf>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    buttons: [
        {
            extend: 'excel',
            text: '<i class="fa fa-arrow-down white"></i> Tải báo cáo'
        }
    ],
    language: table_vi_lang
});
$('.buttons-excel').addClass('btn btn-primary btn-sm mr-1 ');

$('#provider-transaction-mng').on('click', 'button.btn-accept-trans', function () {
    var tr = $(this).closest('tr');
    var row = providerTransactionTable.row(tr);
    var transId = row.data().TransactionId;
    var foodId = row.data().FoodId;
    callAjax(
        {
            url: UPDATE_TRANSACTION_URI + transId,
            dataType: JSON_DATATYPE,
            type: PUT,
        }, JSON.stringify({
            StatusId: 3,
            RejectedReason: ""
        }),
        function (result) {
            callAjax(
                {
                    url: CREATE_PROVIDER_FOOD_URI,
                    dataType: JSON_DATATYPE,
                    type: POST,
                }, JSON.stringify({
                    FoodId: foodId
                }),
                function (result) {
                    toastr.success('Giao dịch thành công');
                });
            $("#provider-transaction-mng").DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});

$('#provider-transaction-mng').on('click', 'button.btn-deny-trans', function () {
    var tr = $(this).closest('tr');
    var row = providerTransactionTable.row(tr);
    var transId = row.data().TransactionId;
    $('#DenyModal').modal('show');
    $('#transId').val(transId);
});

$("#btnAddProviderFood").click(function () {
    var transId = $('#transId').val();
    var reason = $('#RejectedReason').val();
    callAjax(
        {
            url: UPDATE_TRANSACTION_URI + transId,
            dataType: JSON_DATATYPE,
            type: PUT,
        }, JSON.stringify({
            StatusId: 4,
            RejectedReason: reason
        }),
        function (result) {
            toastr.success('Từ chối giao dịch thành công');
            $('#DenyModal').modal('hide');
            $('#RejectedReason').val("");
            $("#provider-transaction-mng").DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});

// Barcode
$('#provider-transaction-mng').on('click', 'button.btn-barcode', function () {
    var tr = $(this).closest('tr');
    var row = providerTransactionTable.row(tr);
    var id = row.data().TransactionId;
    $("#btnPrintBarcode").attr("download", "Transaction-" + id + ".jpg");
    makeCode("Trans-" + id);
    $('#GetQRCode').modal('show');
});

function makeCode(id) {
    JsBarcode("#barcode", "" + id);
}

download_img = function (el) {
    var canvas = document.getElementById("barcode");
    var image = canvas.toDataURL("image/jpg");
    el.href = image;
};