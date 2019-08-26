$('#btnAddVaccine').on('click', function () {
    $('#vaccineList').append(
        '  <div class="input-group mb-1">'
        + ' <input type="hidden" class="form-control vaccine-id" value="0">'
        + ' <input type="text" placeholder="Nhập vac-xin" class="form-control vaccine-name" >'
        + ' <span class="input-group-append">'
        + '   <button class="btn btn-danger" onclick="removeVaccineMng(this)" type="button"><i class="ft-x"></i></button>'
        + ' </span>'
        + '</div>');
})


var listVaccine = [];
function removeVaccineMng(button) {

    var vaccineId = $(button).parent().parent().find('.vaccine-id').val();
    if (vaccineId == 0) {
        $(button).parent().parent().remove();
        return;
    }
    var vaccineName = $(button).parent().parent().find('.vaccine-name').val();
    swal({
        title: "Xóa thức ăn?",
        text: "Bạn muốn xóa " + vaccineName + "!",
        icon: "warning",
        buttons: {
            cancel: {
                text: "Hủy",
                value: null,
                visible: true,
                className: "",
                closeModal: true,
            },
            confirm: {
                text: "Xác nhận",
                value: true,
                visible: true,
                className: "",
                closeModal: false
            }
        }
    }).then((isConfirm) => {
        if (isConfirm) {
            callAjaxAuth(
                {
                    url: REMOVE_VACCINE_URI + vaccineId,
                    dataType: JSON_DATATYPE,
                    type: DELETE
                },
                '',
                function (result) {
                    $(button).parent().parent().remove();
                    swal("Xóa thành công", "", "success");
                },
                function (result) {
                    swal("Xóa thất bại vui lòng thử lại", "", "error");
                }
            )

        }
    });
}

function IsVaccineModified() {
    var isModified = false;
    $('input.vaccine-id').each(function () {
        if ($(this).val() == 0) {
            isModified = true;
        }
    })
    if (isModified) {
        return true;
    } else {
        return false;
    }
}

$('#btnConfirmSaveVaccine').on('click', function () {
    if (!IsVaccineModified()) {
        return;
    }
    $('#mng-vaccine-error').empty();
    var vaccine = [];
    var vaccineIds = [];
    var vaccineNames = [];
    var isDuplicate = false;
    var listDuplicate = [];

    $('input.vaccine-id').each(function () {
        vaccineIds.push($(this).val())
    })
    $('input.vaccine-name').each(function () {
        vaccineNames.push($(this).val())
    })
    vaccineIds.forEach(function (item, index, array) {
        if (vaccineNames[index] != "") {
            vaccine.push({
                'VaccineId': item,
                'VaccineName': vaccineNames[index]
            })
        }
    });
    debugger;
    var isHasNewValue = false;
    vaccine.forEach(function (value, index, array) {
        if (value.VaccineId == 0) {
            isHasNewValue = true;
            var vaccineName = value.VaccineName + "";
            listVaccine.forEach(function (value, index, array) {
                if (vaccineName.toLowerCase() == value) {
                    listDuplicate.push(vaccineName);
                    isDuplicate = true;
                }
            });
        }
    });

    if (isDuplicate) {
        var stringDuplicate = ""
        listDuplicate.forEach(function (value, index, array) {
            stringDuplicate = stringDuplicate + "'" + value + "'" + " ";
        });
        $('#mng-vaccine-error').append('<label class="error"> Vac-xin ' + stringDuplicate + ' đã tồn tại!</label>');
        return;
    }


    if (!isHasNewValue) {
        return;
    }

    callAjaxAuth(
        {
            url: UPDATE_VACCINE_URI,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify(
            vaccine
        ),
        function (result) {
            toastr.success(result.message);
            loadvaccineDataModal();
        },
        function (result) {
            toastr.error(result.message);
        }
    )
    $('#mng-vaccine-error').empty();
})


$('#manageVaccineModal').on('show.bs.modal', function (e) {
    loadvaccineDataModal()
})

var confirmHide = false;
$('#manageVaccineModal').on('hide.bs.modal', function (e) {

    var isModified = IsVaccineModified();

    if (isModified && !confirmHide) {
        e.preventDefault();
        //e.stopImmediatePropagation();
        swal({
            title: "Những chỉnh sửa sẽ không được lưu bạn có muốn tiếp tục ?",
            text: "",
            icon: "warning",
            buttons: {
                cancel: {
                    text: "Quay lại",
                    value: null,
                    visible: true,
                    className: "",
                    closeModal: true,
                },
                confirm: {
                    text: "Đồng ý",
                    value: true,
                    visible: true,
                    className: "",
                    closeModal: true
                }
            }
        }).then((isConfirm) => {
            if (isConfirm) {
                confirmHide = true;
                $(this).modal('hide');
                confirmHide = false;
            }
        });
    }

})

function loadvaccineDataModal() {
    $('#vaccineList').empty();
    listVaccine = [];
    callAjaxAuth(
        {
            url: GET_VACCINE_LIST_BY_PREMISES_URI,
            dataType: JSON_DATATYPE,
            type: GET
        },
        '',
        function (result) {
            if (result == null || result == 'undefind') {
                return;
            }
            $.each(result.data, function (data, value) {
                listVaccine.push(value.VaccineName.toLowerCase());
                $('#vaccineList').append(
                    '  <div class="input-group mb-1">'
                    + ' <input type="hidden" class="form-control vaccine-id" value="' + value.VaccineId + '">'
                    + ' <input type="text" value="' + value.VaccineName + '" readonly class="form-control vaccine-name" >'
                    + ' <span class="input-group-append">'
                    + '   <button class="btn btn-danger" onclick="removeVaccineMng(this)" type="button"><i class="ft-x"></i></button>'
                    + ' </span>'
                    + '</div>');
            })
        },
        function (result) {
            toastr.error(result.message);
        })
}