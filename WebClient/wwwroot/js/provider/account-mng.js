$(document).ready(function () {
    //Validation
    $("#form-add-account").validate({
        rules: {
            Fullname: {
                required: true,
                lettersonly: true,
                minlength: 3,
                maxlength: 50
            },
            Username: {
                required: true,
                username: true,
                minlength: 3,
                maxlength: 20
            },
            Email: {
                required: true,
                email: true,
                maxlength: 250
            }
        },
        messages: {
            Fullname: {
                required: requiredError,
                lettersonly: letterOnlyError,
                minlength: "Vui lòng nhập ít nhất 3 ký tự",
                maxlength: "Vui lòng nhập không quá 50 ký tự"
            },
            Username: {
                required: requiredError,
                username: userError,
                minlength: "Vui lòng nhập ít nhất 3 ký tự",
                maxlength: "Vui lòng nhập không quá 20 ký tự"
            },
            Email: {
                required: requiredError,
                email: emailError,
                maxlength: "Vui lòng nhập không quá 250 ký tự"
            }
        },
        submitHandler: function (form) {
            var fullname = $('#Fullname').val();
            var username = $('#Username').val();
            var email = $('#Email').val();
            console.log(fullname + "-" + username + "-" + email);
            callAjaxAuth(
                {
                    url: MANAGER_CREATE_ACCOUNT_URI,
                    dataType: JSON_DATATYPE,
                    type: POST
                }, JSON.stringify({
                    Username: username,
                    Email: email,
                    Fullname: fullname
                }),
                function (result) {
                    toastr.success('Thêm thành công');
                    $('#add-account').modal('hide');
                    $("#provider-account-mng").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result.responseJSON.message);
                }
            )
        }
    });
});

var providerAccountTable = $('#provider-account-mng').DataTable({
    ajax: {
        url: MANAGER_GET_ACCOUNT_URI,
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
        { data: 'Username', width: '15%' },
        { data: 'Fullname', width: '20%' },
        { data: 'Email', width: '25%' },
        { data: 'PhoneNo', width: '15%' },
        {
            data: function (data, type, dataToSet) {
                if (data.IsActive) {
                    return "<span class='badge badge-glow badge-pill badge-success'>Kích hoạt</span>";
                } else {
                    return "<span class='badge badge-glow badge-pill badge-danger'>Vô hiệu hóa</span>";
                }
            }, width: '15%'
        },
        {
            data: function (data, type, dataToSet) {
                if (data.IsActive) {
                    return '<center><button class="btn btn-danger btn-deactivate" title="Hủy kích hoạt"><i class="fa fa-times"></i></button></center>';
                } else {
                    return '<center><button class="btn btn-success btn-activate" title="Kích hoạt"><i class="fa fa-check"></i></button></center>';
                }
            }, width: '10%'
        }
    ],
    dom: '<"row" <"col-sm-12"Bf>>'
        + '<"row" <"col-sm-12"i>>'
        + '<"row" <"col-sm-12"tr>>'
        + '<"row"<"col-sm-5"l><"col-sm-7"p>>',
    buttons: [
        {
            text: '<i class="fa fa-plus white"></i> Thêm tài khoản',
            className: 'btn btn-primary btn-sm mr-1 btnAccount'
        }
    ],
    language: userTable_vi_lang
});

$('#provider-account-mng').on('click', 'button.btn-deactivate', function () {
    var tr = $(this).closest('tr');
    var row = providerAccountTable.row(tr);
    var id = row.data().UserId;
    swal({
        title: "Hủy kích hoạt?",
        text: "Tài khoản này sẽ bị hủy kích hoạt!",
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
                    url: MANAGER_UPDATE_ACCOUNT_STATUS_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Hủy thành công!", "Tài khoản đã được hủy kích hoạt.", "success");
                    $("#provider-account-mng").DataTable().ajax.reload();
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

$('#provider-account-mng').on('click', 'button.btn-activate', function () {
    var tr = $(this).closest('tr');
    var row = providerAccountTable.row(tr);
    var id = row.data().UserId;
    swal({
        title: "Kích hoạt?",
        text: "Tài khoản này sẽ được kích hoạt!",
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
                    url: MANAGER_UPDATE_ACCOUNT_STATUS_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Kích hoạt thành công!", "Tài khoản đã được kích hoạt.", "success");
                    $("#provider-account-mng").DataTable().ajax.reload();
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

$('.btnAccount').on('click', function () {
    clearAddAccountModal();
    $('#add-account').modal('show');
});

function clearAddAccountModal() {
    $('#Fullname').val("");
    $('#Username').val("");
    $('#Email').val("");
    $('.error').empty("");
}