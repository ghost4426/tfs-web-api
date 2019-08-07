$(document).ready(function () {

    $("#dllFoodDetailType").select2({
        ajax: {
            url: GET_FOOD_DETAIL_TYPE_URI,
            dataType: JSON_DATATYPE,
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
            processResults: function (data, params) {
                return {
                    results: data.results
                };
            },
            cache: false
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
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": 'Bearer ' + Cookies.get('token')
            },
            data: function (params) {
                var query = {
                    search: params.term,
                    type: 'public'
                }
                // Query parameters will be ?search=[term]&type=public
                return query;
            },
            processResults: function (data, params) {
                return {
                    results: data.results
                };
            },
            cache: true
        },
        placeholder: "Chọn nhà cung cấp",
        language: "vi"
    });

    //Load category
    callAjaxAuth(
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

$.fn.dataTable.ext.errMode = 'none';
var farmFoodTable = $('#farm-food-mng').DataTable({
    ajax: {
        url: GET_FARM_FOOD_URI,
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
        { data: 'FoodId', width: '10%' },
        { data: 'CategoryName', width: '20%' },
        { data: 'Breed', width: '25%' },
        {
            data: 'CreateDate', width: '15%',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        {
            width: '20%',
            data: null,
            render: function () {
                var btnDetail = '<button class="btn btn-grey btn-sm btn-view-detail" title="Chi tiết"><i class="icon-eye"></i ></button >\n';
                var btnUpdate = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin"><i class="icon-pencil"></i></button>\n'
                var btnSale = '<button class="btn btn-success btn-sm btn-add-provider" title="Bán sản phẩm"><i class="icon-basket"></i></button>\n'
                var btnBarcode = '<button class="btn btn-secondary btn-sm btn-barcode" title="Barcode"><i class="fa fa-barcode"></i></button>\n '
                return '<div class="col-12">' + btnDetail + btnUpdate + btnSale + btnBarcode + '</div>';
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
            attr: {
                id: 'btnAddNewFood',
                'data-toggle': 'modal',
                'data-target': "#default"
            },
            className: 'btn btn-primary btn-sm mr-1 btnAddNewFood',
        },
        {
            extend: 'excel',
            text: '<i class="fa fa-arrow-down white"></i> Tải báo cáo',
            className: 'btn btn-primary btn-sm mr-1',
        }
    ],
    language: table_vi_lang
});

//$('.buttons-excel, .btn-addNew').addClass('btn btn-primary btn-sm mr-1 ');
$('.btnAddNewFood').attr({ 'data-toggle': 'modal', 'data-target': "#default" });
$('.buttons-excel').removeClass('btn-secondary');

function clearDetailModal() {
    $("#detailTitle").empty();
    $("#add-detail-form").empty();
    $('#dllFoodDetailType').val(null).trigger("change");
}

//Add product
$('#btnAddProduct').click(function () {
    var cate = parseInt($('select[name="NewCategory"]').val());
    var breed = $('input[name="Breed"]').val();
    callAjaxAuth(
        {
            url: CREATE_FOOD_DATA_URI,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify({
            CategoryId: cate,
            Breed: breed
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
var preId = 0;


// Show view food detail modal
$('#farm-food-mng').on('click', 'button.btn-view-detail', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    //if (preId != id) {
    //    //clearDetailModal();
    //}
    //preId = id;

    $('#txtFoodIdView').val(id);
    $('#txtFoodCategoryView').val(row.data().CategoryName);
    $('#txtFoodBreedView').val(row.data().Breed);
    $('#view-food-data').modal('show');
});



// Show add food detail modal
$('#farm-food-mng').on('click', 'button.btn-add-detail', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    if (preId != id) {
        clearDetailModal();
    }
    preId = id;

    $('#txtFoodId').val(id);
    $('#txtFoodCategory').val(row.data().CategoryName);
    $('#txtFoodBreed').val(row.data().Breed);
    $('#add-food-data').modal('show');
});

//Get feeding data
function getFeedingData(foodId) {
    callAjaxAuth(
        {
            url: GET_FOOD_FEEDING_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result != null || typeof result != 'undefined') {
                $.each(result, function (data, value) {
                    $("#detailTitle").append('<div class="input-group mb-1"">'
                        + '<input type="text" readonly class="form-control" value="' + value + '" >'
                        + '</div > ')
                });
            }
        },
        function (result) {
            toastr.error(result);
        });
}

function getVaccinsData(foodId) {
    callAjaxAuth(
        {
            url: GET_FOOD_VACCIN_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result != null || typeof result != 'undefined') {
                $.each(result, function (data, value) {
                    $("#detailTitle").append('<div class="input-group mb-1"">'
                        + '<div class="col-6">'
                        + '<div class="form-group">'
                        +'<label for= "userinput1" >Ngày tiêm:</label >'
                        + '<input type="text" readonly class="form-control" value="' + $.format.date(value.VaccinationDate, "dd-MM-yyyy") + '" >'
                        + '</div > '
                        + '</div > '
                        + '<div class="col-6">'
                        + '<div class="form-group">'
                        + '<label for= "userinput1" >Loại vac-xin:</label >'
                        + '<input type="text" readonly class="form-control" value="' + value.VaccinationType + '" >'
                        + '</div > '
                        + '</div > '
                        + '</div > ')
                });
            }
        },
        function (result) {
            toastr.error(result);
        });
}
// Load form add detail base on dll change
$('#dllFoodDetailType').on('change', function () {
    if (this.value == null) {
        return;
    }
    var foodId = $('#txtFoodId').val();
    switch (this.value) {
        case "2":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Thức Ăn</h4>');
            getFeedingData(foodId);
            loadRepeatForm("add-feeding-form", "Thức ăn", "feedings");
            break;
        case "3":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Vac-xin</h4>');
            getVaccinsData(foodId);
            loadRepeatForm("add-vaccin-form", "Vac-xin", "vaccins");
            break;
        case "4":
            // code block
            break;
    }
});

function loadRepeatForm(repeaterId, placeholder, nameInput) {
    //if (nameInput === "feedings") {
    $("#add-detail-form").append('<div class="col-md-12 contact-repeater" id="' + repeaterId + '">'
        + '<div data-repeater-list="' + nameInput + '" >'
        + '<div class="input-group mb-1" data-repeater-item=>'
        + '<input type="text" placeholder="Nhập ' + placeholder + '" required class="form-control" name="">'
        + '<span class="input-group-append" id="button-addon2">'
        + '<button class="btn btn-danger" type="button" data-repeater-delete=""><i class="ft-x"></i></button>'
        + '</span>'
        + '</div>'
        + '</div>'
        + '<button type="button" data-repeater-create class="btn btn-primary"><i class="ft-plus"></i> Thêm ' + placeholder + ' </button>'
        + '</div>');
    //} else {
    //    $("#add-detail-form").append('<div class="col-md-12 contact-repeater" id="' + repeaterId + '">'
    //        + '<div data-repeater-list="' + nameInput + '" >'
    //        + '<div class="input-group mb-1" data-repeater-item>'
    //        //+ '<div class="col-5">'
    //        //+ '<input type="text" placeholder="Nhập ' + placeholder + '" required class="form-control" name="vacxin" value="A"/>'
    //        //+ '</div>'
    //                + '<div class="col-5">'
    //        + ' <input type="date" name="date" value="2013-01-08"/>'
    //                + '</div>'
    //            + '<span class="input-group-append" id="button-addon2">'
    //                + '<button class="btn btn-danger" type="button" data-repeater-delete=""><i class="ft-x"></i></button>'
    //            + '</span>'
    //        + '</div>'
    //        + '</div class="pull-right">'
    //            + '<button type="button" data-repeater-create class="btn btn-primary"><i class="ft-plus"></i> Thêm ' + placeholder + ' </button>'
    //        + '</div>');
    //}

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

//Call ajax add feeding data
function callAjaxAddFeedingsData(foodId, feedingData) {
    callAjaxAuth(
        {
            url: ADD_FOOD_FEEDING_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: PUT
        }, JSON.stringify(feedingData),
        function (result) {
            toastr.success(result);
        },
        function (result) {
            toastr.error(result);
        }
    )
}


function callAjaxAddVaccinsData(foodId, vaccinData) {
    callAjaxAuth(
        {
            url: ADD_FOOD_VACCIN_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: PUT
        }, JSON.stringify(vaccinData),
        function (result) {
            toastr.success(result);
        },
        function (result) {
            toastr.error(result);
        }
    )
}

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
            var foodId = $('#txtFoodId').val();
            callAjaxAddFeedingsData(foodId, feedingData);
            $('#add-food-data').modal('hide');
            clearDetailModal();
            break;
        case "3":
            var vaccinData = [];
            var value = $('#add-vaccin-form').repeaterVal();
            $.each(value.vaccins, function (data, value) {
                if ($.isArray(value)) {
                    vaccinData.push(value[0]);
                } else {
                    vaccinData.push(value);
                }
            });
            var foodId = $('#txtFoodId').val();
            callAjaxAddVaccinsData(foodId, vaccinData);
            $('#add-food-data').modal('hide');
            clearDetailModal();
            break;
        default:
            break;
    }
});

function clearProviderModal() {
    $('#ddlProvider').val(null).trigger("change");
}

$('#farm-food-mng').on('click', 'button.btn-add-provider', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    if (preId != id) {
        clearProviderModal();
    }
    preId = id;
    $('#pro-food-id').val(id);
    $('#addDistributor').modal('show');
});

$('#btn-addProvider').click(function () {
    var foodId = parseInt($('#pro-food-id').val());
    var providerId = parseInt($('#ddlProvider').val());
    callAjaxAuth(
        {
            url: CREATE_TRANSACTION_URI,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify({
            ReceiverId: providerId,
            FoodId: foodId
        }),
        function (result) {
            toastr.success('Giao dịch thành công, vui lòng chờ bộ phận kiểm dịch và nhà cung cấp xác minh');
            $('#addDistributor').modal('hide');
            $('#ddlProvider').val(null);
            $("#farm-food-mng").DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});

// Barcode
$('#farm-food-mng').on('click', 'button.btn-barcode', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    $("#btnPrintBarcode").attr("download", "Food-" + id + ".jpg");
    makeCode("Food-" + id);
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