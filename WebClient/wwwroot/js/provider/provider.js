$(document).ready(function () {
    // get List of Food
    
});

var providerFoodTable = $('#provider-food-mng').DataTable({
    ajax: {
        url: GET_FOOD_PROVIDER_URI,
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    columns: [
        { data: 'FoodId', width: '10%' },
        { data: 'Food.Category.Name', width: '20%' },
        { data: 'Food.Breed', width: '25%' },
        {
            data: 'CreatedDate', width: '20%',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        {
            width: '15%',
            data: null,
            render: function (o) {
                var btnDetail = '<button class="btn btn-grey btn-sm" data-toggle="modal" data-target="#getinfo" title="Chi tiết"><i class="icon-eye"></i ></button >\n';
                var btnUpdate = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin"><i class="icon-pencil"></i></button>\n'
                return '<div class="col-12">' + btnDetail + btnUpdate + '</div>';
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
})

$('.buttons-excel').addClass('btn btn-primary btn-sm mr-1 ');