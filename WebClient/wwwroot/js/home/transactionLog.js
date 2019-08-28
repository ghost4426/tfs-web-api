
//Load dataTable
$.fn.dataTable.ext.errMode = 'none';
var farmFoodTable = $('#transaction-view').DataTable({
    ajax: {
        url: GET_TRANSACTION_URI,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=utf-8',
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
        { data: 'FoodId', width: '10%' },
        { data: 'TransactionHash', width: '10%' },
        { data: 'Type', width: '15%' },
        { data: 'CreateBy', width: '10%' },
        {
            data: 'CreateDate', width: '15%',
            render: function (data, type, row) {
                return jQuery.format.prettyDate(data)
            }
        },
        {
            width: '5%',
            data: function (data, type, dataToSet) {
                return '<button class="btn btn-primary btn-sm btn-view-transaction-input" title="Chi tiết"><i class="icon-eye"></i ></button >';
            }
        }
    ],
    language: table_vi_lang_transaction
});

$('#transaction-view').on('click', '.btn-view-transaction-input', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var txHash = row.data().TransactionHash;
    callAjax(
        {
            url: GET_TRANSACTION_INPUT_URI + txHash,
            dataType: JSON_DATATYPE,
            type: GET
        },
       "", function (result) {
            $('#transactionInput').val(result.result)
        },
        function (result) {
            toastr.error(result.responseJSON.message);
        }
    );
    $('#view-transaction-input  ').modal('show');
});
