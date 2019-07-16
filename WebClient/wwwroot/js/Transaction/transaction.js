$(document).ready(function () {

});

var transactionTable = $('#transaction-mng').DataTable({
    ajax: {
        url: GET_TRANSACTION_URI,
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    columns: [
        { data: 'TransactionId'},
        { data: 'FoodName' },
        { data: 'FoodBreed'},
        { data: 'Farm'},
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

$('#transaction-mng').on('click', 'tr', function () {
    var tr = $(this).closest('tr');
    var row = transactionTable.row(tr);
    var id = row.data().TransactionId;
    makeCode("Trans-"+id);
    $('#GetQRCode').modal('show');
});

function makeCode(id) {
    JsBarcode("#barcode", "" + id);
}

function exportBarcode() {
   
}