$(document).ready(function () {

    $("#dllFoodDetailType").select2({
        ajax: {
            url: GET_FOOD_DETAIL_TYPE_URI,
            dataType: JSON_DATATYPE,
            data: "",
            processResults: function (data, params) {
                return {
                    results: data.results
                };
            },
            cache: true
        },
        minimumResultsForSearch: Infinity,
        placeholder: "Chọn thông tin",
        language: "vi"
    });

    //load provider
    $("#ddlProvider").select2({
        ajax: {
            url: GET_PROVIDER,
            dataType: JSON_DATATYPE,
            data: function (params) {
                return {
                    q: params.term, // search term
                    //page: params.page
                };
            },
            processResults: function (data, params) {
                return {
                    results: data.results
                };
            },
            cache: true
        },
        placeholder: "Chọn nhà cung cấp",
        language:"vi"
    });

    //Load category
    callAjax(
        {
            url: GET_FOOD_CATEGORY_URI,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            $.each(result, function (data, value) {
                $("#NewCategory").append($("<option></option>").val(value.CategoryId).html(value.Name));
            });
        },
        function (result) {
            toastr.error(result);
        }
    )
    //Load dataTable
   
});

var farmFoodTable = $('#farm-food-mng').DataTable({
    ajax: {
        url: GET_FARM_FOOD_URI,
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    columns: [
        { data: 'FoodId' },
        { data: 'CategoryName' },
        { data: 'Breed' },
        {
            data: 'CreatedDate',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        {
            width: '15%',
            data: null,
            render: function (o) {
                var btnDetail = '<button class="btn btn-grey btn-sm" data-toggle="modal" data-target="#getinfo" title="Chi tiết"><i class="icon-eye"></i ></button > ';
                var btnUpdate = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin"><i class="icon-pencil"></i></button> '
                var btnSale = '<button class="btn btn-success btn-sm" data-toggle="modal" data-target="#addDistributor" title="Bán sản phẩm"><i class="icon-basket"></i></button> '
                return btnDetail + btnUpdate + btnSale;
            }
        }
    ],
    dom: '<"row" <"col-sm-12"Bf>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    buttons: [
        {
            text: '<i class="fa fa-plus white"></i> Thêm mới',
            className: 'btn-addNew',
        },
        {
            extend: 'excel',
            text: '<i class="fa fa-arrow-down white"></i> Tải báo cáo'
        }
    ],
    language: table_vi_lang
});

$('.buttons-excel, .btn-addNew').addClass('btn btn-primary btn-sm mr-1 ');
$('.btn-addNew').attr({ 'data-toggle': 'modal', 'data-target': "#default" });

$('#add-food-data').on('hide.bs.modal', function (e) {
    $('#dllFoodDetailType').val(null).trigger("change");
    $("#detailTitle").empty();
    $("#add-detail-form").empty();
})

$('#btnAddProduct').click(function () {
    var cate = parseInt($('select[name="NewCategory"]').val());
    var breed = $('input[name="Breed"]').val();
    callAjax(
        {
            url: ADD_FOOD,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify({
            CategoryId: cate,
            Breed: breed,
            FarmerId: 1
        }),
        function (result) {
            toastr.success('Thêm thành công');
            $('#default').modal('hide');
            $('select[name="NewCategory"]').val("1");
            $('input[name="Breed"]').val("");
            $("#farm-food-mng").DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});

$('#farm-food-mng').on('click', 'button.btn-add-detail', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    $('#txtFoodId').val(id);
    $('#txtFoodCategory').val(row.data().CategoryName);
    $('#txtFoodBreed').val(row.data().Breed);
    $('#add-food-data').modal('show'); 

});

$('#dllFoodDetailType').on('change', function () {
    $("#detailTitle").empty();
    $("#add-detail-form").empty();
    switch (this.value) {
        case "2":
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Thức Ăn</h4>')
            loadRepeatForm("add-feeding-form", "Thức ăn", "feedings")
            break;
        case "3":
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Vac-xin</h4>')
            loadRepeatForm("add-vaccin-form", "Vac-xin", "vaccins")
            break;
        case "4":
            // code block
            break;
        default:
            break;
    }
});

function loadRepeatForm(repeaterId, placeholder, nameInput) {
    $("#add-detail-form").append('<div class="col-md-12 contact-repeater" id="' + repeaterId + '">'
        + '<div data-repeater-list="' + nameInput + '" >'
        + '<div class="input-group mb-1" data-repeater-item="">'
        + '<input type="text" placeholder="Nhập ' + placeholder + '" required class="form-control" name="">'
        + '<span class="input-group-append" id="button-addon2">'
        + '<button class="btn btn-danger" type="button" data-repeater-delete=""><i class="ft-x"></i></button>'
        + '</span>'
        + '</div>'
        + '</div>'
        + '<button type="button" data-repeater-create class="btn btn-primary"><i class="ft-plus"></i> Thêm ' + placeholder + ' </button>'
        + '</div>');
    $("#" + repeaterId).repeater(
        {
            show: function () {
                $(this).slideDown()
            },
            hide: function (remove) {
                confirm("Bạn có muốn xóa ô này?") && $(this).slideUp(remove)
            },
            isFirstItemUndeletable: true
        })
};

$('#btnAddDetail').on('click', function () {
    //if (!$("#formAddDetail").validate()) {
    //    return false;
    //};
    var value = $("#dllFoodDetailType").val();
    switch (value) {
        case "2":
            var feedingData = [];
            var value = $('#add-feeding-form').repeaterVal();
            $.each(value.feedings, function (data, value) {
                if ($.isArray(value)) {
                    feedingData.push(value[0]);
                } else {
                    feedingData.push(value);
                } 
            });
            console.log(feedingData);
            co
            callAjax(
                {
                    url: ADD_FOOD_FEEDING_DATA_URI,
                    dataType: JSON_DATATYPE,
                    type: GET
                }, JSON.stringify(),
                function (result) {
                    $.each(result, function (data, value) {
                        $("#NewCategory").append($("<option></option>").val(value.CategoryId).html(value.Name));
                    });
                },
                function (result) {
                    toastr.error(result);
                }
            )
            break;
        case "3":
            $("#add-detail-form").empty();
            loadRepeatForm("add-vaccin-form", "vac-xin", "vaccins")
            break;
        case "4":
            // code block
            break;
        default:
            break;
    }
});

function addFeedingData() {
    callAjax(
        {
            url: GET_FOOD_CATEGORY_URI,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            $.each(result, function (data, value) {
                $("#NewCategory").append($("<option></option>").val(value.CategoryId).html(value.Name));
            });
        },
        function (result) {
            toastr.error(result);
        }
    )
}





