$.fn.dataTable.ext.errMode = 'none';
var distFoodTable = $('#dist-food-mng').DataTable({
    ajax: {
        url: DISTRIBUTOR_GET_FOOD,
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
        { data: 'FoodId', width: '5%' },
        { data: 'Food.Category.Name', width: '10%' },
        { data: 'Food.Breed', width: '20%' },
        { data: 'FarmName', width: '22.5%' },
        { data: 'ProviderName', width: '22.5%' },
        {
            data: 'Food.CreateDate', width: '15%',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        {
            width: '5%',
            data: function (data, type, dataToSet) {
                var btnBarcode = '<button class="btn btn-secondary btn-sm btn-barcode" title="Barcode"><i class="fa fa-barcode"></i></button> '
                return '<center>' + btnBarcode + '</center>';
            }
        }
    ],
    dom: '<"row" <"col-sm-12"Bf>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    buttons: [
       
    ],
    language: table_vi_lang
});

$('#dist-food-mng').on('click', 'button.btn-barcode', function () {
    var tr = $(this).closest('tr');
    var row = distFoodTable.row(tr);
    var foodid = row.data().FoodId;
    var providerId = row.data().ProviderId;
    var distributorId = row.data().DistributorId;
    $("#btnPrintBarcode").attr("download", "Food-" + foodid + ".jpg");
    makeCode("Food-" + foodid + "-" + providerId + "-" + distributorId);
    $('#GetQRCode').modal('show');
});

function makeCode(id) {
    JsBarcode("#barcode", "" + id, {
        width: 50,
        height: 1600,
        displayValue: false
    });
}

download_img = function (el) {
    var canvas = document.getElementById("barcode");
    var image = canvas.toDataURL("image/jpg");
    el.href = image;
};
