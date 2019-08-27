   
//Load dataTable
$.fn.dataTable.ext.errMode = 'none';
var farmFoodTable = $('#farm-food-mng').DataTable({
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
        }
    ],
    dom: '<"row" <"col-sm-12"f>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    language: table_vi_lang_transaction
});
