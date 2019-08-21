$(document).ready(function () {
});

var userTable = $('#userTable').DataTable({
    ajax: {
        url: GET_USER_URI,
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
        { data: 'Username' },
        { data: 'Fullname' },
        {
            data: 'Role.Name',
            render: function (data, type, row) {
                if (data == "Manager") {
                    return "Quản lý";
                } else if (data == "Staff") {
                    return "Nhân viên";
                } else if (data == "Veterinary") {
                    return "Kiểm duyệt viên";
                }
            }
        },
        { data: 'PhoneNo' },
        { data: 'Email' },
        {
            data: 'IsActive',
            render: function (data, type, row) {
                if (data == true) {
                    return "<span class='badge badge-glow badge-pill badge-success'>Kích hoạt</span>";
                }
                else {
                    return "<span class='badge badge-glow badge-pill badge-danger'>Vô hiệu hóa</span>";
                };
            }
        },
        {
            data: function (data, type, dataToSet) {
                if (data.IsActive) {
                    return '<center><button class="btn btn-danger btn-deactivate" title="Hủy kích hoạt"><i class="fa fa-times"></i></button></center>';
                } else {
                    return '<center><button class="btn btn-success btn-activate" title="Kích hoạt"><i class="fa fa-check"></i></button></center>';
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
            text: '<i class="fa fa-plus white"></i> Thêm mới Kiểm Duyệt viên',
            
            className: 'btn btn-primary btn-sm mr-1 btnAddNewVeterinary',
        },
    ],
    language: userTable_vi_lang
});
$('.btnAddNewVeterinary').on('click', function () {
    $('#addVeterinary').modal('show');
    
});
$('#confirmButton').click(function () {
    var userId = parseInt($('#txtUserIdActive').val());
    callAjax(
        {
            url: DEACTIVE_USER_URI + userId,
            dataType: JSON_DATATYPE,
            type: PUT,
        }, JSON.stringify(),
        function (result) {
            toastr.success('Bạn đã thay đổi trạng thái thành công', 'Thay đổi thành công');
            $('#confirm').modal('hide');
            $('#userTable').DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result);
        }
    )
});

$('#addNewVeterinaryButton').click(function () {
    var isValidated = true;
    var username = $('#txtVeterinaryName').val();
    var fullname = $('#txtVeterinaryFullname').val();
    var email = $('#txtVeterinaryEmail').val();
    var phone = $('#txtVeterinaryPhone').val();
    if (!validate(username)) {
        $('#userValidate').text("*Vui lòng nhập tài khoản*");
        isValidated = false;
    } else {
        $('#userValidate').text("");
    }
    if (!validate(fullname)) {
        $('#fullnameValidate').text("*Vui lòng nhập Họ và tên*");
        isValidated = false;
    } else {
        $('#fullnameValidate').text("");
    }
    if (!validate(email)) {
        $('#emailValidate').text("*Vui lòng nhập đúng Email*");
        isValidated = false;
    } else {
        $('#emailValidate').text("");
    }
    if (!validate(phone)) {
        $('#phoneValidate').text("*Vui lòng nhập đúng Số điện thoại*");
        isValidated = false;
    } else {
        $('#phoneValidate').text("");
    }
    if (isValidated) {
        Create(username, fullname, email, phone);
    }
});
function validate(input) {
    if (input == null || input == "") {
        return false;
    }
    return true;
};
function Create(username, fullname, email, phone) {
    callAjaxAuth(
        {
            url: CREATE_VETERINARY_URI,
            dataType: JSON_DATATYPE,
            type: POST,
        }, JSON.stringify({
            Username: username,
            Fullname: fullname,
            Email: email,
            Phone: phone
        }),
        function (result) {
            toastr.success('Tạo mới Kiểm Duyệt Viên thành Công', 'Tạo mới thành công');
            $('#addVeterinary').modal('hide');
            clearAddAccountModal();
            $('#userTable').DataTable().ajax.reload();
        },
        function (result) {
            toastr.error(result.message());
        }
    )
};

function getUserInfo(userId) {

    callAjax(
        {
            type: GET,
            url: GET_USER_DETAILS_URI + userId,
            dataType: JSON_DATATYPE,

        },
        JSON.stringify({
        }),
        function (user, result) {
            getRole();
            $('#userId').val(user.data.UserId);
            $('#FullName').val(user.data.Fullname);
            $('#Email').val(user.data.Email);
            $('#Phone').val(user.data.PhoneNo);
            $('#txtUserIdRole').val(user.data.UserId);
            $('div#RoleOption select').val(user.data.RoleId).change();
            $('#txtUserIdActive').val(user.data.UserId);
            if (user.data.IsActive == true) {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái của tài khoản <font style='color:blue;font-weight:bold'>" +
                    user.data.Username +
                    "</font> sang" +
                    "<font style='color:red'> Vô hiệu lực</font> " +
                    " không?";
            } else {
                document.getElementById("statusLabel").innerHTML = "Bạn có muốn thay đổi trạng thái của tài khoản <font style='color:blue;font-weight:bold'>" +
                    user.data.Username +
                    "</font> sang" +
                    "<font style='color:Green'> Hiệu lực</font>" +
                    " không?";
            }
            $('#status').val(user.data.IsActive);
        },
        function (result) {
            toastr.error(result);
        })
    getRole();
}

$('#userTable').on('click', 'button.btn-deactivate', function () {
    var tr = $(this).closest('tr');
    var row = userTable.row(tr);
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
                    url: DEACTIVE_USER_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Hủy thành công!", "Tài khoản đã được hủy kích hoạt.", "success");
                    $("#userTable").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result);
                }
            );
        } else {
            swal("", "Bạn đã hủy hành động này", "error");
        }
    });
});

$('#userTable').on('click', 'button.btn-activate', function () {
    var tr = $(this).closest('tr');
    var row = userTable.row(tr);
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
                    url: DEACTIVE_USER_URI + id,
                    dataType: JSON_DATATYPE,
                    type: PUT
                }, "",
                function (result) {
                    swal("Kích hoạt thành công!", "Tài khoản đã được kích hoạt.", "success");
                    $("#userTable").DataTable().ajax.reload();
                },
                function (result) {
                    toastr.error(result);
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
    $('#txtVeterinaryName').val("");
    $('#txtVeterinaryFullname').val("");
    $('#txtVeterinaryEmail').val("");
    $('#txtVeterinaryPhone').val("");
}
