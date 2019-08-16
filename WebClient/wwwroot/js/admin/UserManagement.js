$(document).ready(function () {
    changeRole();
});

var userTable = $('#userTable').DataTable({
    ajax: {
        url: GET_USER_URI,
        beforeSend: showLoadingPage,
        complete: hideLoadingPage
    },
    'autoWidth': false,
    columns: [
        { data: 'UserId' },
        { data: 'Username' },
        { data: 'Fullname' },
        { data: 'Role.Name' },
        { data: 'PhoneNo' },
        { data: 'Email' },
        {
            data: 'IsActive',
            render: function (data, type, row) {
                if (data == true) {
                    return '<span class="btn btn-success btn-sm mr-1 mb-1 ladda-button"><b>Hiệu lực</b></span>';
                }
                else {
                    return '<span class="btn btn-danger btn-sm mr-1 mb-1 ladda-button"><b>Vô Hiệu lực</b></span>';
                };
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                if (data.IsActive == true) {
                    return '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-danger" data-toggle="modal" data-target="#confirm" tittle="Đổi trạng thái"><i class="fa fa-times"></i ></button >';
                }
                else {
                    return '<button onclick="getUserInfo(' + data.UserId + ')" class="btn btn-success" data-toggle="modal" data-target="#confirm" tittle="Đổi trạng thái"><i class="fa fa-check"></i ></button >';

                };

            }
        },
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
    callAjax(
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

function getRole() {
    $("#dllRole").empty();
    $.ajax({
        url: GET_ROLE_URI,
        type: GET,
        success: function (data) {

            $.each(data, function (key, value) {
                $("#dllRole").append($("<option></option>").val(value.RoleId).html(value.Name));
            });
        },
        error: function () {
            toastr.error('Xin hãy kiểm tra lại', 'Thất bại');
        }
    })
}

//Change Role
function changeRole() {
    $('#changeRoleButton').click(function () {
        var userId = parseInt($('#txtUserIdRole').val());
        var roleId = $('select[id="dllRole"]').val();
        $.ajax({
            type: 'PUT',
            url: CHANGE_ROLE_URI + userId,
            contentType: 'json',
            headers: {
                //'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: roleId,
            success: function () {
                toastr.success('Cập nhật thông tin người dùng thành công', 'Thành Công');
                $('#changeRole').modal('hide');
                $('#userTable').DataTable().ajax.reload();
            },
            error: function () {
                toastr.error('Xin hãy kiểm tra lại', 'Cập nhật thất bại');
            }
        })
    })
}


