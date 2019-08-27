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
                var foodId = parseInt($('#pro-food-id').val());
                var query = {
                    search: params.term,
                    foodId: foodId,
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
            toastr.error(result.responseJSON.message);
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
                    $('#addNewFoodModal').modal('hide');
                    $('select[name="NewCategory"]').val("1");
                    $('input[name="Breed"]').val("");
                    $("#farm-food-mng").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result.responseJSON.message);
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
            data: function (data, type, dataToSet) {
                var btnDetail = '<button class="btn btn-grey btn-sm btn-view-detail" title="Chi tiết"><i class="icon-eye"></i ></button >\n';
                var btnUpdate = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin"><i class="icon-pencil"></i></button>\n'
                var btnSale = '<button class="btn btn-success btn-sm btn-add-provider" title="Bán sản phẩm"><i class="icon-basket"></i></button>\n'
                var btnSoldOut = '<button class="btn btn-danger btn-sm btn-sold-out" title="Hết sản phẩm"><i class="ft-x"></i></button>\n'
                var btnUpdateDis = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin" disabled><i class="icon-pencil"></i></button>\n'
                var btnSaleDis = '<button class="btn btn-success btn-sm btn-add-provider" title="Bán sản phẩm" disabled><i class="icon-basket"></i></button>\n'
                var btnSoldOutDis = '<button class="btn btn-danger btn-sm btn-sold-out" title="Hết sản phẩm" disabled><i class="ft-x"></i></button>\n'
                if (data.IsSoldOut) {
                    return '<div class="col-12">' + btnDetail + btnUpdateDis + btnSaleDis + btnSoldOutDis + '</div>';
                } else if (data.IsReadyForSale) {
                    return '<div class="col-12">' + btnDetail + btnUpdateDis + btnSale + btnSoldOut + '</div>';
                } else {
                    return '<div class="col-12">' + btnDetail + btnUpdate + btnSale + btnSoldOutDis + '</div>';
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
            text: '<i class="fa fa-arrow-down white"></i> Tải báo cáo',
            className: 'btn btn-primary btn-sm mr-1 btn-report',
        }
    ],
    language: table_vi_lang
});

//report
$('.btn-report').click(function () {
    data = {
        ids: [1, 2, 3, 4, 5]
    };
    // Use XMLHttpRequest instead of Jquery $ajax
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        var a;
        var date = new Date();
        var filename = "Báo cáo tháng " + (date.getMonth() + 1);
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            a = document.createElement('a');
            a.href = window.URL.createObjectURL(xhttp.response);
            a.download = filename + ".xlsx";
            a.style.display = 'none';
            document.body.appendChild(a);
            a.click();
        }
    };
    // Post data to URL which handles post request
    xhttp.open("POST", DOWNLOAD_REPORT_URI);
    xhttp.setRequestHeader("Authorization", 'Bearer ' + Cookies.get('token'));
    xhttp.setRequestHeader("Content-Type", "application/json");
    // You should set responseType as blob for binary responses
    xhttp.responseType = 'blob';
    xhttp.send(JSON.stringify(data));
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
            if (result.data == null) {
                return;
            }
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
            toastr.error(result.responseJSON.message);
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



var tmpIdList = [];
// Load form add detail base on dll change
$('#dllFoodDetailType').on('change', function () {
    if (this.value == null) {
        return;
    }
    $('#choose-error').empty();
    $('#error').empty();
    tmpIdList = [];
    var foodId = $('#txtFoodId').val();
    switch (this.value) {
        case "2":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i> Chi Tiết Thức Ăn</h4>');
            getFeedingData(foodId);
            loadRepeatForm("feedings", foodId);
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
            toastr.error(result.responseJSON.message);
        });
}
var currentFoodVaccines = [];
function getVaccinsData(foodId) {
    currentFoodVaccines = [];
    callAjaxAuth(
        {
            url: GET_FOOD_VACCIN_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result != null || typeof result != 'undefined') {
                $.each(result.data, function (data, value) {
                    var vaccineDate = value.VaccinationDate;
                    currentFoodVaccines.push({
                        'vaccineDate': new Date(vaccineDate).getTime(),
                        'vaccineType': value.VaccinationType
                    });
                    vaccineDate = $.format.date(vaccineDate, "dd-MM-yyyy");
                    $("#detailTitle").append('<div class="input-group mb-1"">'
                        + '<div class="col-5">'
                        + '<div class="form-group">'
                        + '<label for= "userinput1" >Ngày tiêm:</label >'
                        + '<input type="text" readonly class="form-control" value="' + vaccineDate + '" >'
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
            toastr.error(result.responseJSON.message);
        });
}

function loadRepeatForm(nameInput, foodId) {
    if (nameInput === "feedings") {
        $("#add-detail-form").append(
            '<select class="form-control" id="dllAddFeedingData"style="width: 90% !important">'
            + '</select>'
            + '<span class="input-group-append" style="width: 10%"><button class="btn btn-primary" onclick="addFeedingToList()" type="button" style="width: 100%"><i class="ft-plus"></i></button> </span>'
        );
        callAjaxAuth(
            {
                url: GET_FEEDING_LIST_BY_FOODID_URI + foodId,
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
                toastr.error(result.responseJSON.message);
            }
        )
    } else {
        var todaysDate = new Date();
        var year = todaysDate.getFullYear();
        var month = ("0" + (todaysDate.getMonth() + 1)).slice(-2);
        var day = ("0" + todaysDate.getDate()).slice(-2);
        var maxDate = (year + "-" + month + "-" + day);
        $("#add-detail-form").append(
            '<input type="date" class="form-control" style="height: 40px !important" max="' + maxDate + '" id="vaccineDate"/>'
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
                toastr.error(result.responseJSON.message);
            }
        )
    }
};

function addFeedingToList() {
    $('#error').empty();
    var feedingId = $('#dllAddFeedingData').val();
    if (feedingId == null) {
        $('#error').append('<label class="error">Vui lòng chọn 1 loại thức ăn!</label>');
        return;
    }
    if (tmpIdList.indexOf(feedingId) < 0) {
        tmpIdList.push(feedingId)
        var feeding = $('#dllAddFeedingData :selected').text();
        $("#detailTitle").append('<div class="input-group mb-1"">'
            + '<input type="hidden" readonly class="form-control feedingId-selected" value="' + feedingId + '">'
            + '<input type="text" readonly class="form-control feeding-selected" value="' + feeding + '" >'
            + ' <span class="input-group-append">'
            + '   <button class="btn btn-danger" onclick="removeFeedingAddDetail(this)" type="button"><i class="ft-x"></i></button>'
            + ' </span>'
            + '</div > ')
        $("#dllAddFeedingData").val(null).trigger("change");
    } else {
        $('#error').append('<label class="error">Bạn không thể chọn thức ăn giống nhau!</label>');
    }

}

function removeFeedingAddDetail(button) {
    var feedingId = $(button).parent().parent().find('.feedingId-selected').val();
    var pos = tmpIdList.indexOf(feedingId);
    tmpIdList.splice(pos, 1);
    $(button).parent().parent().remove();
}


function addVaccineToList() {
    $('#error').empty();
    var isError = false;
    var vaccineId = $('#dllAddVaccineData').val();
    var vaccineDate = $('#vaccineDate').val();
    var vaccine = $('#dllAddVaccineData :selected').text();
    if (vaccineId == null) {
        $('#error').append('<label class="error">Vui lòng chọn 1 loại vac-xin!</label>');
        isError = true;
    }
    if (vaccineDate == null != vaccineDate == "") {
        $('#error').append('<label class="error">Vui lòng chọn ngày tiêm!</label>');
        isError = true;
    }
    vaccineDate = $.format.date(new Date(vaccineDate), "MM-dd-yyyy")
    var isVaccineInfoExist = false;
    var vaccineSelected = {
        'vaccineDate': new Date(vaccineDate).getTime(),
        'vaccineType': vaccine
    }
    currentFoodVaccines.forEach(function (vaccine, index) {
        if (vaccine.vaccineDate === vaccineSelected.vaccineDate && vaccine.vaccineType === vaccineSelected.vaccineType) {
            isVaccineInfoExist = true;
        }
    })
    vaccineDate = $.format.date(new Date(vaccineDate), "dd-MM-yyyy")
    if (isVaccineInfoExist) {
        $('#error').append('<label class="error">Đã tồn tại thông tin:Vac-xin ' + vaccine + ' tiêm vào ngày ' + vaccineDate + '!</label>');
        isError = true;


    }
    if (isError) {
        return;
    }
    appendDataToVaccineList(vaccineId, vaccineDate, vaccine)
}

function appendDataToVaccineList(vaccineId, vaccineDate, vaccine) {
    $("#detailTitle").append(
        '<div class="input-group mb-1">'
        + ' <div class="col-5">'
        + '     <div class="form-group">'
        + '         <label >Ngày tiêm:</label>'
        + '         <input type="text" readonly class="form-control vaccineDate-selected" value="' + vaccineDate + '">'
        + '     </div>'
        + '     </div>'
        + ' <div class="col-5">'
        + '     <div class="form-group">'
        + '         <label>Loại vac-xin:</label>'
        + '         <input type="hidden" readonly class="form-control vaccineId-selected" value="' + vaccineId + '">'
        + '         <input type="text" readonly name="txtVaccineName" class="form-control vaccine-selected" value="' + vaccine + '">'
        + '     </div>'
        + ' </div>'
        + ' <div class="col-2">'
        + '     <div class="form-group">'
        + '         <label for="userinput1">Xóa</label>'
        + '         <button class="btn btn-danger" onclick="removeVaccineAddDetail(this)" type="button"><i class="ft-x"></i></button>'
        + '     </div>'
        + ' </div>'
        + '</div>'
    )
    $("#dllAddVaccineData").val(null).trigger("change");
}

function removeVaccineAddDetail(button) {
    var vaccineId = $(button).parent().parent().find('.vaccineId-selected').val();
    var pos = tmpIdList.indexOf(vaccineId);
    tmpIdList.splice(pos, 1);
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
            toastr.error(result.responseJSON.message);
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
            toastr.error(result.responseJSON.message);
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
            var feedingIds = [];
            var feedingNames = [];
            $('input.feedingId-selected').each(function () {
                feedingIds.push($(this).val());
            });
            $('input.feeding-selected').each(function () {
                feedingNames.push($(this).val());
            });
            if ((feedingIds.length != feedingNames.length)) {
                toastr.error('Đã có lỗi xảy ra vui lòng thử lại');
                return;
            }
            feedingIds.forEach(function (item, index, array) {
                feedingData.push({
                    'FeedingId': item,
                    'Feedingname': feedingNames[index]
                })
            });
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
                var dateArray = $(this).val().split("-");
                var newdate = dateArray[1] + '-' + dateArray[0] + '-' + dateArray[2];
                vaccineDates.push(newdate);
            })
            $('input.vaccine-selected').each(function () {
                vaccineNames.push($(this).val())
            })
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
            break;
        default:
            break;
    }
});

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
function callAjaxAddVaccinsData(foodId, vaccineData) {
    callAjaxAuth(
        {
            url: ADD_FOOD_VACCIN_DATA_URI + foodId,
            dataType: JSON_DATATYPE,
            type: PUT
        }, JSON.stringify(vaccineData),
        function (result) {
            toastr.success(result.message);
        },
        function (result) {
            toastr.error(result.message);
        }
    )
}

//Clear Create transaction modal
function clearProviderModal() {
    $('#pro-error').empty();
    $('#num-error').empty();
    $('#check-pro').val("");
    $('#check-pro-vac').val("");
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
            var certificationNum = result.data.Farm.CertificationNumber;
            if (certificationNum != null) {
                $('#certificationNumber').val(certificationNum);
                $('#certificationNumber').attr('readonly', true);
            } else {
                $('#certificationNumber').val("");
                $('#certificationNumber').attr('readonly', false);
            }
            if (feeding == null) {
                $('#check-pro').val("feeding");
            } else {
                $('#check-pro').val("check");
            }
            if (vacxin == null) {
                $('#check-pro-vac').val("vacxin");
            } else {
                $('#check-pro-vac').val("check");
            }
        },
        function (result) {
            toastr.error(result.responseJSON.message);
        }
    );
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
    $('#pro-error').empty();
    $('#num-error').empty();
    var foodId = parseInt($('#pro-food-id').val());
    var providerId = parseInt($('#ddlProvider').val());
    var number = $('#certificationNumber').val();
    var check = $('#check-pro').val();
    var checkvac = $('#check-pro-vac').val();
    if ($('#ddlProvider').val() == null || $('#ddlProvider').val() == "") {
        $('#pro-error').append('<label class="error">Vui lòng chọn một nhà cung cấp</label>');
    } else if ($('#certificationNumber').val() == "") {
        $('#num-error').append('<label class="error">Vui lòng không bỏ trống trường này</label>');
    } else if (check != "check") {
        $('#num-error').append('<label class="error">Chưa có dữ liệu thức ăn</label>');
    } else if (checkvac != "check") {
        $('#num-error').append('<label class="error">Chưa có dữ liệu vacxin</label>');
    } else {
        callAjaxAuth(
            {
                url: CREATE_TRANSACTION_URI,
                dataType: JSON_DATATYPE,
                type: POST
            }, JSON.stringify({
                ReceiverId: providerId,
                FoodId: foodId,
                CertificationNumber: number
            }),
            function (result) {
                toastr.success('Giao dịch thành công, vui lòng chờ bộ phận kiểm dịch và nhà cung cấp xác minh');
                $('#addDistributor').modal('hide');
                clearProviderModal();
                $("#farm-food-mng").DataTable().ajax.reload();
            },
            function (result) {
                toastr.error(result.responseJSON.message);
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

// Sold out
$('#farm-food-mng').on('click', 'button.btn-sold-out', function () {
    var tr = $(this).closest('tr');
    var row = farmFoodTable.row(tr);
    var id = row.data().FoodId;
    swal({
        title: "Hết hàng?",
        text: "Sản phẩm này đã hết hàng!",
        icon: "warning",
        showCancelButton: true,
        buttons: {
            cancel: {
                text: "Không",
                visible: true,
                className: "btn-warning",
                closeModal: false,
            },
            confirm: {
                text: "Xác nhận!",
                visible: true,
                className: "",
                closeModal: false
            }
        }
    }).then(isConfirm => {
        if (isConfirm) {
            callAjaxAuth(
                {
                    url: SOLD_OUT_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Cập nhật thành công!", "Sản phẩm này đã hết, không thể tạo thêm giao dịch.", "success");
                    $("#farm-food-mng").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result.responseJSON.message);
                }
            );
        } else {
            swal("", "Bạn đã hủy hành động này", "error");
        }
    });
});