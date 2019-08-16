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

    //Load category add new food
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
    //Validate
    //Add new food
    $("#add-new-food-form").validate({
        rules: {
            Breed: {
                required: true,
                minlength: 3,
                lettersonly: true
            }
        },
        messages: {
            Breed: {
                required: requiredError,
                minlength: "Vui lòng nhập ít nhất 3 ký tự",
                lettersonly: letterOnlyError
            }
        },
        submitHandler: function (form) {
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
        }
    });
});

//Load dataTable
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
                return jQuery.format.prettyDate(data)
            }
        },
        {
            width: '20%',
            data: null,
            render: function () {
                var btnDetail = '<button class="btn btn-grey btn-sm btn-view-detail" title="Chi tiết"><i class="icon-eye"></i ></button >\n';
                var btnUpdate = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin"><i class="icon-pencil"></i></button>\n'
                var btnSale = '<button class="btn btn-success btn-sm btn-add-provider" title="Bán sản phẩm"><i class="icon-basket"></i></button>\n'
                return '<div class="col-12">' + btnDetail + btnUpdate + btnSale + '</div>';
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
            className: 'btn btn-primary btn-sm mr-1 btn-add-new-food',
        },
        {
            text: '<i class="fa fa-pagelines white"></i> Quản lý thức ăn',
            className: 'btn btn-primary btn-sm mr-1 btn-manage-feeding',
        },
        {
            text: '<i class="fa fa-medkit white"></i> Quản lý vac-xin',
            className: 'btn btn-primary btn-sm mr-1 btn-manage-vaccine',
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
$('.btn-add-new-food').attr({ 'data-toggle': 'modal', 'data-target': "#addNewFoodModal" });
$('.btn-manage-feeding').attr({ 'data-toggle': 'modal', 'data-target': "#manageFeedingModal" });
$('.btn-manage-vaccine').attr({ 'data-toggle': 'modal', 'data-target': "#manageVaccineModal" });
$('.buttons-excel').removeClass('btn-secondary');

function clearDetailModal() {
    $("#detailTitle").empty();
    $("#add-detail-form").empty();
    $('#dllFoodDetailType').val(null).trigger("change");
}

var preId = 0;

function clearViewDetailModel() {
    $("#feedding-info").empty();
    $("#feedding-info").append('<div class="form-control col-12"><h5>Không có thông tin</h5></div>');
    $("#vaccine-info").empty();
    $("#vaccine-info").append('<div class="form-control col-12"><h5>Không có thông tin</h5></div>');
}

// Show view food detail modal
$('#farm-food-mng').on('click', 'button.btn-view-detail', function () {
    clearViewDetailModel()
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    callAjaxAuth(
        {
            url: GET_FOODDATA_BY_ID_URI,
            dataType: JSON_DATATYPE,
            type: GET
        },
        {
            id: id
        }, function (result) {
            var feeding = result.data.Farm.Feedings;
            var vacxin = result.data.Farm.Vaccinations;
            if (feeding != null) {
                $("#feedding-info").empty();
                $.each(feeding, function (data, value) {
                    $('#feedding-info').append('<div class="input-group mb-1"">'
                        + '<input type="text" readonly class="form-control" value="' + value + '" >'
                        + '</div > ');
                });
            }
            if (vacxin != null) {
                $("#vaccine-info").empty();
                $.each(vacxin, function (data, value) {
                    $('#vaccine-info').append('<div class="input-group mb-1"">'
                        + '<div class="col-6">'
                        + '<div class="form-group">'
                        + '<label for= "userinput1" >Ngày tiêm:</label >'
                        + '<input type="text" readonly class="form-control" value="' + $.format.date(value.VaccinationDate, "dd-MM-yyyy") + '" >'
                        + '</div > '
                        + '</div > '
                        + '<div class="col-6">'
                        + '<div class="form-group">'
                        + '<label for= "userinput1" >Loại vac-xin:</label >'
                        + '<input type="text" readonly class="form-control" value="' + value.VaccinationType + '" >'
                        + '</div > '
                        + '</div > '
                        + '</div > ');
                });
            }
        },
        function (result) {
            toastr.error(result.message);
        }
    );

    $('#txtFoodIdView').val(id);
    $('#txtFoodCategoryView').val(row.data().CategoryName);
    $('#txtFoodBreedView').val(row.data().Breed);
    $('#view-food-data').modal('show');
});



// Show add food detail modal
$('#farm-food-mng').on('click', 'button.btn-add-detail', function () {
    $('#choose-error').empty();
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
                $.each(result.data, function (data, value) {
                    $("#detailTitle").append('<div class="input-group mb-1"">'
                        + '<input type="text" readonly class="form-control" value="' + value + '" >'
                        + '</div > ')
                });
            }
        },
        function (result) {
            toastr.error(result.message);
        });
}

//Get vaccine data
function getVaccinsData(foodId) {
    callAjaxAuth(
        {
            url: GET_FOOD_VACCIN_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result != null || typeof result != 'undefined') {
                $.each(result.data, function (data, value) {
                    $("#detailTitle").append('<div class="input-group mb-1"">'
                        + '<div class="col-5">'
                        + '<div class="form-group">'
                        + '<label for= "userinput1" >Ngày tiêm:</label >'
                        + '<input type="text" readonly class="form-control" value="' + $.format.date(value.VaccinationDate, "dd-MM-yyyy") + '" >'
                        + '</div > '
                        + '</div > '
                        + '<div class="col-7">'
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
            toastr.error(result.message);
        });
}


// Load form add detail base on dll change
$('#dllFoodDetailType').on('change', function () {
    if (this.value == null) {
        return;
    }
    $('#choose-error').empty();
    $('#error').empty();
    var foodId = $('#txtFoodId').val();
    switch (this.value) {
        case "2":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Thức Ăn</h4>');
            getFeedingData(foodId);
            loadRepeatForm("feedings");
            break;
        case "3":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Vac-xin</h4>');
            getVaccinsData(foodId);
            loadRepeatForm("vaccins");
            break;
    }
});

function loadRepeatForm(nameInput) {
    if (nameInput === "feedings") {
        $("#add-detail-form").append(
            '<select class="form-control" id="dllAddFeedingData"style="width: 90% !important">'
            + '</select>'
            + '<span class="input-group-append" style="width: 10%"><button class="btn btn-primary" onclick="addFeedingToList()" type="button" style="width: 100%"><i class="ft-plus"></i></button> </span>'
        );
        callAjaxAuth(
            {
                url: GET_FEEDING_LIST_URI,
                dataType: JSON_DATATYPE,
                type: GET
            }, "",
            function (result) {
                var data = $.map(result.results, function (obj) {
                    obj.id = obj.id;
                    return obj;
                });
                $("#dllAddFeedingData").select2({
                    data: data,
                    placeholder: "Chọn thông tin",
                    language: "vi"
                });
            },
            function (result) {
                toastr.error(result.message);
            }
        )
    } else {
         var todaysDate = new Date(); 
        var year = todaysDate.getFullYear();                        
        var month = ("0" + (todaysDate.getMonth() + 1)).slice(-2);  
        var day = ("0" + todaysDate.getDate()).slice(-2);           
        var maxDate = (year + "-" + month + "-" + day);
        $("#add-detail-form").append(
            '<input type="date" class="form-control" style="height: 40px !important" max="' + maxDate +'" id="vaccineDate"/>'
            + '<select class="form-control " id="dllAddVaccineData">'
            + '</select>'
            + '<span class="input-group-append" style="width: 10%"><button class="btn btn-primary" onclick="addVaccineToList()" type="button" style="width: 100%"><i class="ft-plus"></i></button> </span>'
        );
       
        //$('#vaccineDate').attr('max', maxDate);

    callAjaxAuth(
        {
            url: GET_VACCINE_LIST_URI,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            var data = $.map(result.results, function (obj) {
                obj.id = obj.id;
                return obj;
            });
            $("#dllAddVaccineData").select2({
                data: data,
                placeholder: "Chọn thông tin",
                language: "vi"
            });
        },
        function (result) {
            toastr.error(result.message);
        }
    )
}};

function addFeedingToList() {
    var text = $('#dllAddFeedingData :selected').text();
    $("#detailTitle").append('<div class="input-group mb-1"">'
        + '<input type="text" readonly class="form-control feeding-selected" value="' + text + '" >'
        + ' <span class="input-group-append">'
        + '   <button class="btn btn-danger" onclick="removeFeedingAddDetail(this)" type="button"><i class="ft-x"></i></button>'
        + ' </span>'
        + '</div > ')
    $("#dllAddFeedingData").val(null).trigger("change");
}

function removeFeedingAddDetail(button) {
    $(button).parent().parent().remove();
}

function addVaccineToList() {
    var vaccineId = $('#dllAddVaccineData').val();
    var vaccine = $('#dllAddVaccineData :selected').text();
    var vaccineDate = $('#vaccineDate').val();
    $("#detailTitle").append(
        '<div class="input-group mb-1">'
        + '<div class="col-5">'
        + '<div class="form-group">'
        + ' <label >Ngày tiêm:</label>'
        + '<input type="text" readonly class="form-control vaccineDate-selected" value="' + $.format.date(new Date(vaccineDate), "dd-MM-yyyy") + '">'
        + '</div>'
        + '</div>'
        + '<div class="col-5">'
        + '<div class="form-group">'
        + '<label>Loại vac-xin:</label>'
        + '<input type="hidden" readonly class="form-control vaccineId-selected" value="' + vaccineId + '">'
        + '<input type="text" readonly name="txtVaccineName" class="form-control vaccine-selected" value="' + vaccine + '">'
        + '</div>'
        + '</div>'
        + '<div class="col-2">'
        + '<div class="form-group">'
        + '<label for="userinput1">Xóa</label>'
        + '<button class="btn btn-danger" onclick="removeVaccineAddDetail(this)" type="button"><i class="ft-x"></i></button>'
        + '</div>'
        + '</div>'
        + '</div>'
    )
    $("#dllAddVaccineData").val(null).trigger("change");
}

function removeVaccineAddDetail(button) {
    $(button).parent().parent().parent().remove();
}

//Call ajax add feeding data
function callAjaxAddFeedingsData(foodId, feedingData) {
    callAjaxAuth(
        {
            url: ADD_FOOD_FEEDING_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: PUT
        }, JSON.stringify(feedingData),
        function (result) {
            toastr.success(result.message);
        },
        function (result) {
            toastr.error(result.message);
        }
    )
}

//Call ajax add Vaccine data
function callAjaxAddVaccinsData(foodId, vaccinData) {
    callAjaxAuth(
        {
            url: ADD_FOOD_VACCIN_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: PUT
        }, JSON.stringify(vaccinData),
        function (result) {
            toastr.success(result.message);
        },
        function (result) {
            toastr.error(result.message);
        }
    )
}


//Click btn add detail add food data
$('#btnAddDetail').on('click', function () {
    $('#error').empty();
    $('#choose-error').empty();
    var value = $("#dllFoodDetailType").val();
    if (value == null || value == "") {
        $('#choose-error').append('<label id="Breed-error" class="error" for="Breed">Vui lòng chọn một thông tin</label>');
    }
  
    switch (value) {
        case "2":
            var feedingData = [];
            $('input.feeding-selected').each(function () {
                feedingData.push($(this).val())
            })
            var foodId = $('#txtFoodId').val();
                callAjaxAddFeedingsData(foodId, feedingData);
                $('#add-food-data').modal('hide');
                clearDetailModal();
            break;
        case "3":
            var vaccineData = [];
            var vaccineIds = [];
            var vaccineDates = [];
            var vaccineNames = [];
            $('input.vaccineId-selected').each(function () {
                vaccineIds.push($(this).val())
            })
            $('input.vaccineDate-selected').each(function () {
                vaccineDates.push($(this).val())
            })
            $('input.vaccine-selected').each(function () {
                vaccineNames.push($(this).val())
            })
            debugger;
            if (vaccineIds.length != vaccineDates.length && vaccineIds.length != vaccineNames.length) {
                toastr.error('Đã có lỗi xảy ra vui lòng thử lại');
                return;
            }
            vaccineIds.forEach(function (item, index, array) {
                vaccineData.push({
                    'VaccineId': item,
                    'VaccineName': vaccineNames[index],
                    'VaccineDate': vaccineDates[index],
                })
            });
            var foodId = $('#txtFoodId').val();
            callAjaxAddVaccinsData(foodId, vaccineData);
            clearDetailModal();
            $('#add-food-data').modal('hide');
            // if (check == 0) {
            //     callAjaxAddVaccinsData(foodId, vaccinData);
            //     $('#add-food-data').modal('hide');
            //     clearDetailModal();
            // } else if (check == 1) {
            //     $('#error').append('<label class="error">Vui lòng nhập các trường</label>');
            // } else if (check == 2) {
            //     $('#error').append('<label class="error">Vui lòng không nhập ký tự đặc biệt</label>');
            // }
            break;
        default:
            break;
    }
});


//Clear Create transaction modal
function clearProviderModal() {
    $('#pro-error').empty();
    $('#ddlProvider').val(null).trigger("change");
}

$('#ddlProvider').on('change', function () {
    $('#pro-error').empty();
});

$('#farm-food-mng').on('click', 'button.btn-add-provider', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    var name = row.data().CategoryName;
    var breed = row.data().Breed;
    if (preId != id) {
        clearProviderModal();
    }
    preId = id;
    $('#pro-food-id').val(id);
    $('#pro-name').val(name);
    $('#pro-breed').val(breed);
    $('#addDistributor').modal('show');
});

$('#btn-addProvider').click(function () {
    var foodId = parseInt($('#pro-food-id').val());
    var providerId = parseInt($('#ddlProvider').val());
    if ($('#ddlProvider').val() == null || $('#ddlProvider').val() == "") {
        $('#pro-error').append('<label class="error">Vui lòng chọn một nhà cung cấp</label>');
    } else {
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
                clearProviderModal();
                $("#farm-food-mng").DataTable().ajax.reload();
            },
            function (result) {
                toastr.error(result);
            }
        )
    }
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


