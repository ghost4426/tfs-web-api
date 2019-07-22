$(document).ready(function () {
    // get List of Food
    $("#dllFoodDetailType").select2({
        ajax: {
            url: GET_PROVIDER_FOOD_DETAIL_TYPE_URI,
            dataType: JSON_DATATYPE,
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
            extend: 'excel',
            text: '<i class="fa fa-arrow-down white"></i> Tải báo cáo'
        }
    ],
    language: table_vi_lang
})

$('.buttons-excel').addClass('btn btn-primary btn-sm mr-1 ');

$('#provider-food-mng').on('click', 'button.btn-barcode', function () {
    var tr = $(this).closest('tr');
    var row = providerFoodTable.row(tr);
    var id = row.data().FoodId;
    makeCode("Food-" + id);
    $('#GetQRCode').modal('show');
});

function makeCode(id) {
    JsBarcode("#barcode", "" + id);
}

//$('#btnPrintBarcode').on('click', function () {
//    var imageData;
//    var newData;
//    html2canvas(document.querySelector("#printJs-form")).then(canvas => {
//        imgageData = canvas.toDataURL("image/png");
//        newData = imageData.replace(/^data:image\/png/, "data:application/octet-stream");        
//    });
//    $("#btnPrintBarcode").attr("download", "image.png").attr("href", newData);
//});

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
            loadRepeatForm("add-treatment-form", "bước", "treatment");
            loadTreatmentData(foodId);
            break;
        case "7":
            $("#detailTitle").empty();
            $("#add-detail-form").empty();
            $("#detailTitle").append('<h4 class="form-section"><i class="ft-info"></i>Thông Tin Đóng Gói</h4>');
            loadPackingForm();
            break;
    }
});

function loadTreatmentData(foodId) {
    callAjax(
        {
            url: GET_FOOD_TREATMENT_URI + foodId,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result.data.length != 0) {
                $('#txtTreatmentName').val(result.data[0].Name);
                $('#txtTreatmentName').attr('readonly', true);
                for (var i = 1; i < result.data.length; i++) {
                    $('#TreatmentProcessForm').append('<div class="row form-group">'
                        + '<label class="col-md-2 label-control" for="txtFoodId">Bước ' + i + '</label>'
                        + '<div class="col-md-10">'
                        + '<input class="form-control" type="text" value="' + result.data[i].Name + '" readonly />'
                        + '</div>'
                        + '</div>');
                }
            }
        },
        function (result) {
            toastr.error(result);
        }
    )
}

function loadRepeatForm(repeaterId, placeholder, nameInput) {
    $("#add-detail-form").append('<div class="col-md-12 form-group"><input type="text" placeholder="Nhập tên quy trình" class="form-control" id="txtTreatmentName"/></div>'
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

function loadPackingForm() {
    $("#add-detail-form").append('<label class="col-md-4 label-control">Ngày sản xuất</label>'
        + '<input class="col-md-6 form-control" type="date" id="txtMFGDate"/>'
        + '<label class="col-md-4 label-control">Hạn sử dụng</label>'
        + '<input class="col-md-6 form-control" type="date" id="EXPDate"/>');
};

$('#addDetailConfirm').on('click', function () {
    var treatmentData = [];
    var treatmentName = $('#txtTreatmentName').val();
    var foodId = $('#txtFoodId').val();
    var value = $('#add-treatment-form').repeaterVal();
    $.each(value.treatment, function (data, value) {
        if ($.isArray(value)) {
            treatmentData.push(value[0]);
        } else {
            treatmentData.push(value);
        }
    });
    if ($('#txtTreatmentName').is('[readonly]')) {
        callAjaxAddMoreTreatmentData(foodId, treatmentData);
    } else {
        callAjaxAddTreatmentData(foodId, treatmentName, treatmentData);
    }    
    $('#addinfo').modal('hide');
    clearDetailModal();
});

function callAjaxAddTreatmentData(foodId, treatmentName, treatmentProcess) {
    callAjax(
        {
            url: ADD_FOOD_TREATMENT_URI + foodId,
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

function callAjaxAddMoreTreatmentData(foodId, treatmentProcess) {
    callAjax(
        {
            url: ADD_MORE_FOOD_TREATMENT_URI + foodId,
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

// show detail modal
$('#provider-food-mng').on('click', 'button.btn-detail', function () {
    clearViewDetailModal();
    var tr = $(this).closest('tr');
    var row = providerFoodTable.row(tr);
    var id = row.data().FoodId;
    $('#getinfo').modal('show');
    $('input[name="txtFoodId"]').val(id);
    $('#input[name="txtFoodCategory"]').val(row.data().Food.Category.Name);
    $('#input[name="txtFoodBreed"]').val(row.data().Food.Breed);
    callAjax(
        {
            url: GET_FOOD_TREATMENT_URI+id,
            dataType: JSON_DATATYPE,
            type: GET
        }, "",
        function (result) {
            if (result.data.length != 0) {
            $('#TreatmentName').val(result.data[0].Name);
            for (var i = 1; i < result.data.length; i++) {
                $('#TreatmentProcess').append('<div class="row form-group">'
                    + '<label class="col-md-2 label-control" for="txtFoodId">Bước ' + i + '</label>'
                    + '<div class="col-md-10">'
                    + '<input class="form-control" type="text" value="' + result.data[i].Name + '" readonly />'
                    + '</div>'
                    + '</div>');
                }
            }
        },
        function (result) {
            toastr.error(result);
        }
    )
});

function clearViewDetailModal() {
    $('#TreatmentName').val("");
    $('#TreatmentProcess').empty();
}