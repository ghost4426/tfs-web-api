var farmTransactionTable = $('#farm-transaction-mng').DataTable({
    ajax: {
        url: GET_FARM_TRANSACTION_URI,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',
            "Authorization": 'Bearer ' + Cookies.get('token')
        },
        statusCode: {
            401: function () {
                window.location.replace("/dang-nhap");
            },
        },
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    ordering: false,
    columns: [
        { data: 'TransactionId' },
        { data: 'FoodName' },
        { data: 'FoodBreed' },
        { data: 'Provider' },
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
            data: null,
            render: function (o) {
                var btnBarcode = '<button class="btn btn-secondary btn-sm btn-barcode" title="Barcode"><i class="fa fa-barcode"></i></button> '
                return '<div class="col-12">' + btnBarcode + '</div>';
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

// Barcode
//$('#farm-transaction-mng').on('click', 'button.btn-barcode', function () {
//    var tr = $(this).closest('tr');
//    var row = farmTransactionTable.row(tr);
//    var id = row.data().TransactionId;
//    $("#btnPrintBarcode").attr("download", "Trans-" + id + ".jpg");
//    makeCode("Trans-" + id);
//    $('#GetQRCode').modal('show');
//});

//function makeCode(id) {
//    JsBarcode("#barcode", "" + id, {
//        width: 50,
//        height: 1600,
//        displayValue: false
//    });
//}

//download_img = function (el) {
//    var canvas = document.getElementById("barcode");
//    var image = canvas.toDataURL("image/jpg");
//    el.href = image;
//};