$('#btnAddFeeding').on('click', function () {
    $('#feedingList').append(
        '  <div class="input-group mb-1">'
        + ' <input type="hidden" class="form-control feeding-id" value="0">'
        + ' <input type="text" placeholder="Nhập thức ăn" class="form-control feeding-name" >'
        + ' <span class="input-group-append">'
        + '   <button class="btn btn-danger" onclick="removeFeedingMng(this)" type="button"><i class="ft-x"></i></button>'
        + ' </span>'
        + '</div>');
})


var listFeeding = [];
function removeFeedingMng(button) {

    var feedingId = $(button).parent().parent().find('.feeding-id').val();
    if (feedingId == 0) {
        $(button).parent().parent().remove();
        return;
    }
    var feedingName = $(button).parent().parent().find('.feeding-name').val();
    swal({
        title: "Xóa thức ăn?",
        text: "Bạn muốn xóa " + feedingName + "!",
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
                    url: REMOVE_FEEDING_URI + feedingId,
                    dataType: JSON_DATATYPE,
                    type: PUT
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

function IsFeedingModified() {
    var isModified = false;
    $('input.feeding-id').each(function () {
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

$('#btnConfirmSaveFeeding').on('click', function () {
    $('#mng-feeding-error').empty();
    if (!IsFeedingModified()) {
        return;
    }
    var feeding = [];
    var feedingIds = [];
    var feedingNames = [];
    var isDuplicate = false;
    var listDuplicate = []

    $('input.feeding-id').each(function () {
        feedingIds.push($(this).val())
    })
    
    $('input.feeding-name').each(function () {
        var feeding = $(this).val()
        feedingNames.push(feeding);
    })
    

    feedingIds.forEach(function (item, index, array) {
        if (feedingNames[index] != "") {
            feeding.push({
                'FeedingId': item,
                'FeedingName': feedingNames[index]
            })
        }
    });
    var isHasNewValue = false;
    feeding.forEach(function (value, index, array) {
        if (value.FeedingId == 0) {
            isHasNewValue = true;
           var feedingName = value.FeedingName +"";
            listFeeding.forEach(function (value, index, array) {
                if (feedingName.toLowerCase() == value) {
                    listDuplicate.push(feedingName);
                    isDuplicate = true;
                }
            });
        }
    });

    if (isDuplicate) {
        var stringDuplicate = ""
        listDuplicate.forEach(function (value, index, array) {
            stringDuplicate = stringDuplicate +"'"+ value +"'"+ " ";
        });
        $('#mng-feeding-error').append('<label class="error"> Thức ăn ' + stringDuplicate + ' đã tồn tại!</label>');
        return;
    }


    if (!isHasNewValue) {
        return;
    }

    callAjaxAuth(
        {
            url: UPDATE_FEEDING_URI,
            dataType: JSON_DATATYPE,
            type: POST
        }, JSON.stringify(
            feeding
        ),
        function (result) {
            toastr.success(result.message);
            loadFeedingDataModal();
        },
        function (result) {
            toastr.error(result.responseJSON.message);
        }
    )
    $('#mng-feeding-error').empty();
})


$('#manageFeedingModal').on('show.bs.modal', function (e) {
    loadFeedingDataModal()
})

var confirmHide = false;
$('#manageFeedingModal').on('hide.bs.modal', function (e) {

    var isModified = IsFeedingModified();

    if (isModified && !confirmHide) {
        e.preventDefault();
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

function loadFeedingDataModal() {
    $('#feedingList').empty();
    listFeeding = [];
    callAjaxAuth(
        {
            url: GET_FEEDING_LIST_BY_PREMISES_URI,
            dataType: JSON_DATATYPE,
            type: GET
        },
        '',
        function (result) {
            if (result == null || result == 'undefind') {
                return;
            }
            $.each(result.data, function (data, value) {
                listFeeding.push(value.FeedingName.toLowerCase());
                $('#feedingList').append(
                    '  <div class="input-group mb-1">'
                    + ' <input type="hidden" class="form-control feeding-id" value="' + value.FeedingId + '">'
                    + ' <input type="text" value="' + value.FeedingName + '" readonly class="form-control feeding-name" >'
                    + ' <span class="input-group-append">'
                    + '   <button class="btn btn-danger" onclick="removeFeedingMng(this)" type="button"><i class="ft-x"></i></button>'
                    + ' </span>'
                    + '</div>');
            })
        },
        function (result) {
            toastr.error(result.responseJSON.message);
        })
}