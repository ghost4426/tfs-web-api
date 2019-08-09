$(document).ready(function () {
    // get List of Food
    $("#dllFoodDetailType").select2({
        ajax: {
            url: GET_PROVIDER_FOOD_DETAIL_TYPE_URI,
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
            cache: false
        },
        minimumResultsForSearch: Infinity,
        placeholder: "Chọn thông tin",
        language: "vi"
    });

    // get list of treatment
    $("#ddlTreatment").select2({
        ajax: {
            url: GET_TREATMENT_URI,
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
            cache: false
        },
        minimumResultsForSearch: Infinity,
        placeholder: "Chọn thông tin",
        language: "vi"
    });

    //List distributor
    $("#ddlDistributor").select2({
        ajax: {
            url: GET_DISTRIBUTOR_URI,
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
            cache: false
        },
        placeholder: "Chọn nhà phân phối",
        language: "vi"
    });

    //Validation
    $("#formAddDetail").validate({
        rules: {
            dllFoodDetailType: {
                required: true
            },
            ddlChooseTreatment: "required",
            txtMFGDate: {
                required: true,
                smallerThan: true
            },
            EXPDate: {
                required: true,
                greaterThan: function () { return $('#txtMFGDate').val(); }
            }
        },
        messages: {
            dllFoodDetailType: {
                required: "Vui lòng chọn một thông tin"
            },
            ddlChooseTreatment: "Vui lòng chọn một quy trình",
            txtMFGDate: {
                required: "Vui lòng chọn ngày sản xuất",
                smallerThan: "Vui lòng không chọn ngày lớn hơn ngày hiện tại"
            },
            EXPDate: {
                required: "Vui lòng chọn hạn sử dụng",
                greaterThan: "Vui lòng chọn hạn sử dụng lớn hơn ngày sản xuất"
            }
        },
        submitHandler: function (form) {
            var option = $('#dllFoodDetailType').val();
            var foodId = $('#txtFoodId').val();
            if (option == 6) {
                var treatmentId = $('#ddlChooseTreatment').val();
                callAjaxAuth(
                    {
                        url: UPDATE_FOOD_TREATMENT_URI + foodId,
                        dataType: JSON_DATATYPE,
                        type: PUT
                    }, JSON.stringify(treatmentId),
                    function (result) {
                        toastr.success("Thêm thông tin thành công");
                        clearDetailModal();
                        $('#addinfo').modal('hide');
                    },
                    function (result) {
                        toastr.error(result);
                    }
                );
            } else if (option == 7) {
                var mfg = $('#txtMFGDate').val();
                var exp = $('#EXPDate').val();
                callAjaxAuth(
                    {
                        url: ADD_FOOD_PACKAGING_URI + foodId,
                        dataType: JSON_DATATYPE,
                        type: PUT
                    }, JSON.stringify({
                        MFGDate: mfg,
                        EXPDate: exp
                    }),
                    function (result) {
                        toastr.success("Thêm thông tin thành công");
                        clearDetailModal();
                        $('#addinfo').modal('hide');
                    },
                    function (result) {
                        toastr.error(result);
                    }
                );
            }
        }
    });

    $("#printJs-form").validate({
        rules: {
            Distributor: {
                required: true,
                checkTreatment: function () { return $('#checkTreatment').val(); } ,
                checkPackaging: function () { return $('#checkPackaging').val(); },
                checkEXPDate: function () { return $('#checkPackaging').val(); }
            }
        },
        messages: {
            Distributor: {
                required: "Vui lòng chọn một nhà phân phối",
                checkTreatment: "Thực phẩm chưa có thông tin quy trình xử lý",
                checkPackaging: "Thực phẩm chưa có thông tin đóng gói",
                checkEXPDate: "Thực phẩm đã quá hạn sử dụng"
            }
        },
        submitHandler: function (form) {
            var foodId = $('#foodId-trans').val();
            var distributorId = $('#ddlDistributor').val();
            callAjaxAuth(
                {
                    url: PROVIDER_CREATE_TRANSACTION_URI,
                    dataType: JSON_DATATYPE,
                    type: POST
                }, JSON.stringify({
                    ReceiverId: distributorId,
                    FoodId: foodId
                }),
                function (result) {
                    toastr.success("Tạo giao dịch thành công, vui lòng chờ nhà phân phối xác nhận");
                    $('#GetQRCode').modal('hide');
                },
                function (result) {
                    toastr.error(result);
                }
            );
        }
    });
});

$.fn.dataTable.ext.errMode = 'none';
var providerFoodTable = $('#provider-food-mng').DataTable({
    ajax: {
        url: GET_FOOD_PROVIDER_URI,
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
    order: [[3, "desc"]],
    columns: [
        { data: 'FoodId', width: '10%' },
        { data: 'Food.Category.Name', width: '20%' },
        { data: 'Food.Breed', width: '25%' },
        {
            data: 'CreateDate', width: '20%',
            render: function (data, type, row) {
                return $.format.date(data, "dd-MM-yyyy HH:mm")
            }
        },
        {
            width: '15%',
            data: null,
            render: function (o) {
                var btnDetail = '<button class="btn btn-grey btn-sm btn-detail" title="Chi tiết"><i class="icon-eye"></i ></button >\n';
                var btnUpdate = '<button class="btn btn-info btn-sm btn-add-detail" title="Thêm thông tin"><i class="icon-pencil"></i></button>\n'
                var btnBarcode = '<button class="btn btn-secondary btn-sm btn-barcode" title="Barcode"><i class="fa fa-barcode"></i></button> '
                return '<div class="col-12">' + btnDetail + btnUpdate + btnBarcode + '</div>';
            }
        }
    ],
    dom: '<"row" <"col-sm-12"Bf>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    buttons: [
        {
            text: '<i class="fa fa-list-alt white"></i> Quản lý quy trình',
            className: 'btn btn-primary btn-sm mr-1 btnTreatment',
        },
        {
            extend: 'excel',
            text: '<i class="fa fa-arrow-down white"></i> Tải báo cáo',
            className: 'btn btn-primary btn-sm mr-1',
        }
    ],
    language: table_vi_lang
})

$('.buttons-excel').addClass('btn btn-primary btn-sm mr-1 ');
$('.buttons-excel').removeClass('btn-secondary');

// Show add food data modal
var preId = 0;
// Show add food detail modal
$('#provider-food-mng').on('click', 'button.btn-add-detail', function () {
    var tr = $(this).closest('tr');
    var row = providerFoodTable.row(tr);
    var id = row.data().FoodId;
    if (preId != id) {
        clearDetailModal();
    }
    preId = id;

    $('#txtFoodId').val(id);
    $('#txtFoodCategory').val(row.data().Food.Category.Name);
    $('#txtFoodBreed').val(row.data().Food.Breed);
    $('#txtTreatment').val(row.data().TreatmentId);
    $('#addinfo').modal('show');
});

function clearDetailModal() {
    $("#detailTitle").empty();
    $("#add-detail-form").empty();
    $('#dllFoodDetailType').val(null).trigger("change");
}

// Choose detail type option
$('#dllFoodDetailType').on('change', function () {
    if (this.value == null) {
        return;
    }
    var foodId = $('#txtFoodId').val();
    switch (this.value) {
        case "6":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#TreatmentProcessForm").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i>Quy Trình Xử Lý</h4>');
            loadTreatmentForm();
            break;
        case "7":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i>Thông Tin Đóng Gói</h4>');
            loadPackingForm();
            break;
    }
});

function loadTreatmentData(treatmentId) {
    callAjaxAuth(
        {
            url: GET_FOOD_TREATMENT_URI + treatmentId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result.data.length != 0) {
                for (var i = 0; i < result.data.length; i++) {
                    $('#TreatmentProcessData').append('<div class="row form-group">'
                        + '<label class="col-md-2 label-control" for="txtFoodId">Bước ' + (i + 1) + '</label>'
                        + '<div class="col-md-8">'
                        + '<input class="form-control" name="treatment-process" type="text" value="' + result.data[i].Name + '"/>'
                        + '</div>'
                        + '<button type="button" class="btn btn-danger col-md-1 btn-delete-treatment"><i class="ft-x"></i></button>'
                        + '</div>');
                }
                $('.btn-delete-treatment').on('click', function () {
                    $(this).parent().remove();
                });
            }
        },
        function (result) {
            toastr.error(result);
        }
    )
}

$('.btnTreatment').on('click', function () {
    $("#add-treatment-form").empty();
    $('#treatmentModal').modal('show');
    loadRepeatForm("add-treatment-form", "bước", "treatment");
});

$('#btn-confirm').on('click', function () {
    var choose = $('a[data-toggle="tab"].active').text();
    if (choose == 'Thêm mới') {
        var treatmentData = [];
        var treatmentName = $('#txtTreatmentName').val();
        var value = $('#add-treatment-form').repeaterVal();
        $.each(value.treatment, function (data, value) {
            if ($.isArray(value)) {
                treatmentData.push(value[0]);
            } else {
                treatmentData.push(value);
            }
        });
        console.log(treatmentName + "-" + treatmentData);
        callAjaxAddTreatmentData(treatmentName, treatmentData);
        $('#treatmentModal').modal('hide');
    } else if (choose == 'Cập nhật') {
        var treatmentData = [];
        var treatmentId = parseInt($('#ddlTreatment').val());
        $('input[name="treatment-process"').each(function () {
            treatmentData.push($(this).val());
        });
        var value = $('#add-more-treatment-form').repeaterVal();
        $.each(value.treatment, function (data, value) {
            if ($.isArray(value)) {
                treatmentData.push(value[0]);
            } else {
                treatmentData.push(value);
            }
        });
        callAjaxAddMoreTreatmentData(treatmentId, treatmentData);
        $('#TreatmentProcessData').empty();
        $('#MoreTreatment').empty();
        $('#treatmentModal').modal('hide');
    }
});

$('#ddlTreatment').on('change', function () {
    if (this.value == null) {
        return;
    }
    var treatmentId = parseInt($('#ddlTreatment').val());
    $('#TreatmentProcessData').empty();
    $('#MoreTreatment').empty();
    loadTreatmentData(treatmentId);
    loadRepeatFormAddMoreTreatment("add-more-treatment-form", "bước", "treatment");
});

//$('#ddlFixTreatment').on('change', function () {
//    if (this.value == null) {
//        return;
//    }
//    var treatmentId = parseInt($('#ddlFixTreatment').val());
//    $('#FixTreatmentProcessData').empty();
//    loadFixTreatmentData(treatmentId);
//});

function loadRepeatForm(repeaterId, placeholder, nameInput) {
    $("#add-treatment-form").append('<div class="col-md-12 form-group"><input type="text" name="TreatmentName" placeholder="Nhập tên quy trình" class="form-control" id="txtTreatmentName" required/></div>'
        + '<div id = "TreatmentProcessForm" class="col-md-12 form-group"></div > '
        + '<div class="col-md-12 contact-repeater" id="' + repeaterId + '">'
        + '<div data-repeater-list="' + nameInput + '" >'
        + '<div class="input-group mb-1" data-repeater-item="">'
        + '<input type="text" placeholder="Nhập ' + placeholder + '" required class="form-control" name="">'
        + '<span class="input-group-append" id="button-addon2">'
        + '<button class="btn btn-danger" type="button" data-repeater-delete=""><i class="ft-x"></i></button>'
        + '</span>'
        + '</div>'
        + '</div>'
        + '<button type="button" data-repeater-create class="btn btn-primary"><i class="ft-plus"></i> Thêm các bước </button>'
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

function loadRepeatFormAddMoreTreatment(repeaterId, placeholder, nameInput) {
    $("#MoreTreatment").append('<div id = "TreatmentProcessFormMore" class="col-md-12 form-group"></div > '
        + '<div class="col-md-12 contact-repeater" id="' + repeaterId + '">'
        + '<div data-repeater-list="' + nameInput + '" >'
        + '<div class="input-group mb-1" data-repeater-item="">'
        + '<input type="text" placeholder="Nhập ' + placeholder + '" required class="form-control" name="" required>'
        + '<span class="input-group-append" id="button-addon2">'
        + '<button class="btn btn-danger" type="button" data-repeater-delete=""><i class="ft-x"></i></button>'
        + '</span>'
        + '</div>'
        + '</div>'
        + '<button type="button" data-repeater-create class="btn btn-primary"><i class="ft-plus"></i> Thêm các bước </button>'
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

function loadTreatmentForm() {
    var treatmentId = $('#txtTreatment').val();
    $("#add-detail-form").append('<label class="col-md-4 label-control">Chọn quy trình xử lý</label>'
        + '<div class="col-md-12">'
        + '<select class="select2" style="width: 100%" id="ddlChooseTreatment"></select>'
        + '</div>');
    //provider choose treatment
    $("#ddlChooseTreatment").select2({
        ajax: {
            url: GET_TREATMENT_URI,
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
            cache: false
        },
        minimumResultsForSearch: Infinity,
        placeholder: "Chọn thông tin",
        language: "vi"
    });

    $('#ddlChooseTreatment').on("change", function () {
        var treatmentId = parseInt($('#ddlChooseTreatment').val());
        callAjaxAuth({
            url: GET_FOOD_TREATMENT_URI + treatmentId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
            function (result) {
                if (result.data.length != 0) {
                    for (var i = 0; i < result.data.length; i++) {
                        $('#add-detail-form').append('<label class="col-md-2 mb-1 mt-1 label-control" for="txtFoodId">Bước ' + (i + 1) + '</label>'
                            + '<div class="col-md-10 mb-1 mt-1">'
                            + '<input class="form-control" type="text" value="' + result.data[i].Name + '" readonly />'
                            + '</div>');
                    }
                }
            },
            function (result) {
                toastr.error(result);
            }
        );
    });

    if (treatmentId != "") {
        $("#add-detail-form").empty();
        callAjaxAuth(
            {
                url: GET_FOOD_TREATMENT_URI + treatmentId,
                dataType: JSON_DATATYPE,
                type: GET
            }, "",
            function (result) {
                if (result.data.length != 0) {
                    for (var i = 0; i < result.data.length; i++) {
                        $('#add-detail-form').append('<label class="col-md-2 mb-1 label-control" for="txtFoodId">Bước ' + (i + 1) + '</label>'
                            + '<div class="col-md-10 mb-1">'
                            + '<input class="form-control" type="text" value="' + result.data[i].Name + '" readonly />'
                            + '</div>');
                    }
                }
            },
            function (result) {
                toastr.error(result);
            }
        )
    }
}

$('#addDetailConfirm').on('click', function () {
    var foodId = $('#txtFoodId').val();
    var choose = $('#dllFoodDetailType').val();
    if (choose == 6) {
        var treatmentId = $('#ddlChooseTreatment').val();
        callAjaxAuth(
            {
                url: UPDATE_FOOD_TREATMENT_URI + foodId,
                dataType: JSON_DATATYPE,
                type: PUT
            }, JSON.stringify(treatmentId),
            function (result) {
                toastr.success("Thêm thông tin thành công");
                clearDetailModal();
                $('#addinfo').modal('hide');
            },
            function (result) {
                toastr.error(result);
            }
        );
    } else if (choose == 7) {
        var mfg = $('#txtMFGDate').val();
        var exp = $('#EXPDate').val();
        callAjaxAuth(
            {
                url: ADD_FOOD_PACKAGING_URI + foodId,
                dataType: JSON_DATATYPE,
                type: PUT
            }, JSON.stringify({
                MFGDate: mfg,
                EXPDate: exp
            }),
            function (result) {
                toastr.success("Thêm thông tin thành công");
                clearDetailModal();
                $('#addinfo').modal('hide');
            },
            function (result) {
                toastr.error(result);
            }
        );
    }

});

function callAjaxAddTreatmentData(treatmentName, treatmentProcess) {
    callAjaxAuth(
        {
            url: ADD_FOOD_TREATMENT_URI,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify({
            Name: treatmentName,
            TreatmentProcess: treatmentProcess
        }),
        function (result) {
            toastr.success("Thêm thông tin thành công");
        },
        function (result) {
            toastr.error(result);
        }
    )
}

function callAjaxAddMoreTreatmentData(treatmentId, treatmentProcess) {
    callAjaxAuth(
        {
            url: ADD_MORE_FOOD_TREATMENT_URI + treatmentId,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify({
            TreatmentProcess: treatmentProcess
        }),
        function (result) {
            toastr.success("Thêm thông tin thành công");
        },
        function (result) {
            toastr.error(result);
        }
    )
}

function loadPackingForm() {
    var id = $('#txtFoodId').val();
    callAjaxAuth(
        {
            url: PROVIDER_GET_FOOD_DATA_URI,
            dataType: JSON_DATATYPE,
            type: GET
        },
        {
            id: id
        }, function (result) {
            var packaging = result.data.Providers[0].Packaging;
            if (packaging != null) {
                $("#add-detail-form").append('<label class="col-md-4 label-control">Ngày sản xuất</label>'
                    + '<input class="col-md-6 mb-1 form-control" type="date" id="txtMFGDate" value="' + $.format.date(packaging.PackagingDate, "yyyy-MM-dd") + '" readonly/>'
                    + '<label class="col-md-4 label-control">Hạn sử dụng</label>'
                    + '<input class="col-md-6 mb-1 form-control" type="date" id="EXPDate" value="' + $.format.date(packaging.EXPDate, "yyyy-MM-dd") + '" readonly/>');
            } else {
                $("#add-detail-form").append('<label class="col-md-4 label-control">Ngày sản xuất</label>'
                    + '<input class="col-md-6 mb-1 form-control" type="date" id="txtMFGDate"/>'
                    + '<label class="col-md-4 label-control">Hạn sử dụng</label>'
                    + '<input class="col-md-6 mb-1 form-control" type="date" id="EXPDate"/>');
            }
        },
        function (result) {
            toastr.error(result);
        }
    );
};

// show detail modal
$('#provider-food-mng').on('click', 'button.btn-detail', function () {
    clearViewDetailModal();
    var tr = $(this).closest('tr');
    var row = providerFoodTable.row(tr);
    var id = row.data().FoodId;
    $('#getinfo').modal('show');
    $('input[name="txtFoodId"]').val(id);
    $('input[name="txtFoodCategory"]').val(row.data().Food.Category.Name);
    $('input[name="txtFoodBreed"]').val(row.data().Food.Breed);
    callAjaxAuth(
        {
            url: PROVIDER_GET_FOOD_DATA_URI,
            dataType: JSON_DATATYPE,
            type: GET
        },
        {
            id: id
        }, function (result) {
            var feeding = result.data.Farm.Feedings;
            var vacxin = result.data.Farm.Vaccinations;
            var treatment = result.data.Providers[0].Treatment;
            var packaging = result.data.Providers[0].Packaging;
            if (feeding != null) {
                $.each(feeding, function (data, value) {
                    $('#FeedingInfo').append('<div class="input-group mb-1"">'
                        + '<input type="text" readonly class="form-control" value="' + value + '" >'
                        + '</div > ');
                });
            }
            if (vacxin != null) {
                $.each(vacxin, function (data, value) {
                    $('#VacxinInfo').append('<div class="input-group mb-1"">'
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
            if (treatment != null) {
                $.each(treatment.TreatmentProcess, function (data, value) {
                    $('#TreatmentInfo').append('<label class="col-md-2 mb-1 label-control" for="txtFoodId">Bước ' + (data + 1) + '</label>'
                        + '<div class="col-md-10 mb-1">'
                        + '<input class="form-control" type="text" value="' + value + '" readonly />'
                        + '</div>');
                });
            }
            if (packaging != null) {
                $('#txtMFG').val($.format.date(packaging.PackagingDate, "dd-MM-yyyy"));
                $('#txtEXP').val($.format.date(packaging.EXPDate, "dd-MM-yyyy"));
            }
        },
        function (result) {
            toastr.error(result);
        }
    );
});

function clearViewDetailModal() {
    $('#FeedingInfo').empty();
    $('#VacxinInfo').empty();
    $('#TreatmentInfo').empty();
    $('#txtMFG').val("");
    $('#txtEXP').val("");
}

//Barcode
$('#provider-food-mng').on('click', 'button.btn-barcode', function () {
    $('#ddlDistributor').val(null).trigger("change");
    $('#barcode').empty();
    var tr = $(this).closest('tr');
    var row = providerFoodTable.row(tr);
    var foodId = row.data().FoodId;
    callAjaxAuth(
        {
            url: PROVIDER_GET_FOOD_DATA_URI,
            dataType: JSON_DATATYPE,
            type: GET
        },
        {
            id: foodId
        }, function (result) {
            var treatment = result.data.Providers[0].Treatment;
            var packaging = result.data.Providers[0].Packaging.EXPDate;
            $('#checkTreatment').val(treatment);
            $('#checkPackaging').val($.format.date(packaging, "dd-MM-yyyy"));
        },
        function (result) {
            toastr.error(result);
        }
    );
    $('#foodId-trans').val(foodId);
    $('#category-trans').val(row.data().Food.Category.Name);
    $('#breed-trans').val(row.data().Food.Breed);
    makeCode("Food-0-0-0");
    $('#ddlDistributor').on("change", function () {
        if (this.value == null) {
            return;
        }
        var token = Cookies.get('token');
        var providerId = jwt_decode(token).premisesID;
        var distributorId = this.value;
        makeCode("Food-" + foodId + "-" + providerId + "-" + distributorId);
    });
    $("#btnPrintBarcode").attr("download", "Food-" + foodId + ".jpg");
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